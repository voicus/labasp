namespace Phone_Market.DTO
{
    public class ShoppingCartDto
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        public double ProductPrice { get; set; }
        public int ProductDiscount { get; set; }

        public byte[] ProductImage { get; set; }
    }
}
