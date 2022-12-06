﻿using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application.Interfaces
{
    public interface IShopService
    {
        public List<Product> GetAllProducts();
        
        public Product CreateProduct(ProdDTO dto);

        public Product UpdateProduct(int productID, ProdDTO dto);
        
        public Product DeleteProduct(int productID);

        public Product GetProductByID(int productID);
        
        public void RebuildDB();
    }
}

