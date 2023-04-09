using AutoMapper;
using DeliveryAPI.DataContext;
using DeliveryAPI.Domain;
using DeliveryAPI.Models.Requests;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Enums;

namespace DeliveryAPI.Services
{
    public class DeliveryService : IDeliveryService
    {
        private DeliveryDataContext _dbContext { get; set; }
        private readonly IMapper _mapper;

        public DeliveryService(DeliveryDataContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
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
            _dbContext.Deliveries.Remove(toDelete);
            return await _dbContext.SaveChangesAsync();
        }

        public async  Task<Delivery> UpdateAsync(Delivery delivery)
        {
            var upd = _dbContext.Deliveries.Update(delivery);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(upd.Entity);
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
    }
}
