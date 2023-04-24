using AutoMapper;
using DeliveryAPI.DataContext;
using DeliveryAPI.Domain;
using DeliveryAPI.Models.Requests;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Enums;
using SharedLibrary.Messages;

namespace DeliveryAPI.Services
{
    public class DeliveryService : IDeliveryService
    {
        private DeliveryDataContext _dbContext { get; set; }
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeliveryService(DeliveryDataContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _dbContext = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Delivery> ConfirmDelivery(Guid deliveryId)
        {
            var delivery = await _dbContext.Deliveries.SingleOrDefaultAsync(x => x.Id == deliveryId);
            delivery.Status = DeliveryStatuses.Confirmed;
            await _dbContext.SaveChangesAsync();
            return delivery;
        }

        public async Task<Delivery> DeclineDelivery(Guid deliveryId)
        {
            var delivery = await _dbContext.Deliveries.SingleOrDefaultAsync(x => x.Id == deliveryId);
            delivery.Status = DeliveryStatuses.Declined;
            await _dbContext.SaveChangesAsync();
            return delivery;
        }

        public async Task<IEnumerable<Delivery>> GetAll()
        {
            return await _dbContext.Deliveries.ToListAsync();
        }

        public async Task<Delivery> GetByIdAsync(Guid id)
        {
            return await _dbContext.Deliveries.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Delivery> CreateAsync(CreateDeliveryRequest delivery)
        {
            var toCreate = _mapper.Map<Delivery>(delivery);
            var res = await _dbContext.Deliveries.AddAsync(toCreate);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var toDelete = await _dbContext.Deliveries.FirstOrDefaultAsync(x => x.Id == id);
            var deletedEntity = _dbContext.Deliveries.Remove(toDelete);
            var deleteMessage = _mapper.Map<DeliveryDeletedMessage>(deletedEntity.Entity);
            await _publishEndpoint.Publish(deleteMessage);
            return await _dbContext.SaveChangesAsync();
        }

        public async  Task<Delivery> UpdateAsync(Delivery delivery)
        {
            var upd = _dbContext.Deliveries.Update(delivery);
            var updateMessage = _mapper.Map<DeliveryDataChangedMessage>(upd.Entity);
            await _publishEndpoint.Publish(updateMessage);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(upd.Entity);
        }

        public async Task ConsumeDeliveryCreateMessage(DeliveryCreateMessage message)
        {
            var delivery = _mapper.Map<Delivery>(message);
            var tmpList = new List<DeliveryItem>();
            foreach(var item in message.Items)
            {
                var tmpItem = _mapper.Map<DeliveryItem>(item);
                tmpItem.Delivery = delivery;
                tmpList.Add(tmpItem);
            }
            delivery.Items = tmpList;
            delivery.Status = DeliveryStatuses.Pending;
            await _dbContext.Deliveries.AddAsync(delivery);
            await _dbContext.SaveChangesAsync();
        }
    }

    public interface IDeliveryService
    {
        Task<Delivery> GetByIdAsync(Guid id);
        Task<IEnumerable<Delivery>> GetAll();
        Task<Delivery> DeclineDelivery(Guid deliveryId);
        Task<Delivery> ConfirmDelivery(Guid deliveryId);
        Task<Delivery> CreateAsync(CreateDeliveryRequest req);
        Task<int> DeleteAsync(Guid id);
        Task<Delivery> UpdateAsync(Delivery req);
        Task ConsumeDeliveryCreateMessage(DeliveryCreateMessage message);
    }
}
