using MassTransit;
using OrderAPI.Services;
using SharedLibrary.Messages;

namespace OrderAPI.Consumers
{
    public class ProductDeletedMessageConsumer : IConsumer<ProductDeletedMessage>
    {
        private IOrderItemService _orderItemService;
        public ProductDeletedMessageConsumer(IOrderItemService orderService)
        {
            this._orderItemService = orderService;
        }

        public async Task Consume(ConsumeContext<ProductDeletedMessage> context)
        {
            await _orderItemService.ProductDeletedMessageConsume(context.Message);
        }
    }
}
