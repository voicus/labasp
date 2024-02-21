namespace Phone_Market.DTO
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int BrandId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public int Discount { get; set; }
        public string Gender { get; set; }
        public int GenderId { get; set; }

        public double AverageRating { get; set; }
        public string Color { get; set; }

        public byte[] CoverImage { get; set; }

        public List<byte[]> Images { get; set; }
        public List<GiveRatingModel> Ratings { get; set; }
    }
}

