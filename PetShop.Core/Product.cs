﻿using System.ComponentModel.DataAnnotations;

namespace PetShop.Domain
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
        public List<SpecsDescription> SpecsDescriptions { get; set; }
        public int MainCategory { get; set; }
        public int SubCategory { get; set; }        
        public int Brand { get; set; }
        

    }
}