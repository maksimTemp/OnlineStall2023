using MassTransit;
using OrderAPI.Services;
using SharedLibrary.Messages;

namespace OrderAPI.Consumers
{
    public class DeliveryStatusChangedConsumer : IConsumer<DeliveryStatusChangedMessage>
    {
        private IOrderService _orderService;
        public DeliveryStatusChangedConsumer (IOrderService orderService)
        {
            this._orderService = orderService;
        }

        public async Task Consume(ConsumeContext<DeliveryStatusChangedMessage> context)
        {
            await _orderService.DeliveryStatusChangedMessageConsume(context.Message);
        }
    }
}
