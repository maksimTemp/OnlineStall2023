//using MassTransit;
//using DeliveryAPI.Services;
//using SharedLibrary.Messages;

//namespace DeliveryAPI.Consumers
//{
//    public class ItemChangedConsumer : IConsumer<ItemChangedMessage>
//    {
//        private IDeliveryItemService _orderItemService;
//        public ItemChangedConsumer(IOrderItemService orderItemService)
//        {
//            this._orderItemService = orderItemService;
//        }

//        public async Task Consume(ConsumeContext<ItemChangedMessage> context)
//        {
//            await _orderItemService.ConsumeItemChangedMessage(context.Message);
//        }
//    }
//}
