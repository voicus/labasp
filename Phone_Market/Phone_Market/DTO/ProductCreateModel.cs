using System.ComponentModel.DataAnnotations;

namespace Phone_Market.DTO
{
    

        public class ProductCreateModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int BrandId { get; set; }

            [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
            public double Price { get; set; }
            public int CategoryId { get; set; }
            public int ColorId { get; set; }
            public List<IFormFile> Images { get; set; }

    }
}
