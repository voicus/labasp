﻿namespace Phone_Market.DTO
{
    public class EditProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }


    }
}
