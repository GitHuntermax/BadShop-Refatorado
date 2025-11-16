namespace BadShopRefatorado
{
    public class OrderItem
    {
        public int ProductId { get; }
        public int Quantity { get; }
        public double UnitPrice { get; }

        public OrderItem(int productId, int qty, double price)
        {
            ProductId = productId;
            Quantity = qty;
            UnitPrice = price;
        }

        public double Total => UnitPrice * Quantity;
    }
}
