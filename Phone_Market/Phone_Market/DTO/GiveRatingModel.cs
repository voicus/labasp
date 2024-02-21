namespace Phone_Market.DTO
{
    public class GiveRatingModel
    {
        public Guid ProductId { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public bool IsRated { get; set; }

    }
}
