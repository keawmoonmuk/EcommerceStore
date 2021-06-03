namespace Core.Entities.OrderAddregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOdered itemOdered, decimal price, int quantity)
        {
            ItemOrdered = itemOdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOdered ItemOrdered {get; set;}
        public decimal Price { get;set;}
        public int Quantity { get;set;}
    }
}