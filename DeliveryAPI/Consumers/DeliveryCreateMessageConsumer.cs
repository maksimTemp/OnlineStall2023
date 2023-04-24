using MassTransit;
using DeliveryAPI.Services;
using SharedLibrary.Messages;

namespace DeliveryAPI.Consumers
{
    public class DeliveryCreateMessageConsumer : IConsumer<DeliveryCreateMessage>
    {
        private IDeliveryService _deliveryService;
        public DeliveryCreateMessageConsumer(IDeliveryService deliveryService)
        {
            this._deliveryService = deliveryService;
        }

        public async Task Consume(ConsumeContext<DeliveryCreateMessage> context)
        {
            await _deliveryService.ConsumeDeliveryCreateMessage(context.Message);
        }
    }
}
