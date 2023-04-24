using MassTransit;
using DeliveryAPI.Services;
using SharedLibrary.Messages;

namespace DeliveryAPI.Consumers
{
    public class ItemChangedConsumer : IConsumer<ItemChangedMessage>
    {
        private IDeliveryItemService _deliveryItemService;
        public ItemChangedConsumer(IDeliveryItemService deliveryItemService)
        {
            this._deliveryItemService = deliveryItemService;
        }

        public async Task Consume(ConsumeContext<ItemChangedMessage> context)
        {
            await _deliveryItemService.ConsumeItemChangedMessage(context.Message);
        }
    }
}
