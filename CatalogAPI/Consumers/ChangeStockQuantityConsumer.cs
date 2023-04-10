using MassTransit;
using CatalogAPI.Services;
using SharedLibrary.Messages;

namespace CatalogAPI.Consumers
{
    public class ChangeStockQuantityConsumer : IConsumer<ChangeStockQuantityMessage>
    {
        private IProductsService _productsService;
        public ChangeStockQuantityConsumer (IProductsService productsService)
        {
            this._productsService = productsService;
        }

        public async Task Consume(ConsumeContext<ChangeStockQuantityMessage> context)
        {
            await _productsService.ChangeStockQuantityMessageConsume(context.Message);
        }
    }
}
