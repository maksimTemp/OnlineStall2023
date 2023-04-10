using MassTransit;
using OrderAPI.Services;
using SharedLibrary.Messages;

namespace OrderAPI.Consumers
{
    public class ItemChangedConsumer : IConsumer<ItemChangedMessage>
    {
        private IOrderItemService _orderItemService;
        public ItemChangedConsumer(IOrderItemService orderItemService)
        {
            this._orderItemService = orderItemService;
        }

        public async Task Consume(ConsumeContext<ItemChangedMessage> context)
        {
            await _orderItemService.ConsumeItemChangedMessage(context.Message);
        }
    }
}
