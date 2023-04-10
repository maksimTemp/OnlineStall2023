namespace SharedLibrary.Messages
{
    public class ChangeStockQuantityMessage
    {
        public List<(Guid, int)> ProductsQuantity { get; set; }
    }
}
