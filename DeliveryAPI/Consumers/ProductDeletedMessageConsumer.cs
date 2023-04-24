using MassTransit;
using DeliveryAPI.Services;
using SharedLibrary.Messages;

namespace DeliveryAPI.Consumers
{
    public class ProductDeletedMessageConsumer : IConsumer<ProductDeletedMessage>
    {
        private IDeliveryItemService _deliveryItemService;
        public ProductDeletedMessageConsumer(IDeliveryItemService deliveryService)
        {
            this._deliveryItemService = deliveryService;
        }

        public async Task Consume(ConsumeContext<ProductDeletedMessage> context)
        {
            await _deliveryItemService.ProductDeletedMessageConsume(context.Message);
        }
    }
}
