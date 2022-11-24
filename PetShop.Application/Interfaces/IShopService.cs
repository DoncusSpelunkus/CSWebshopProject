using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factory.Application.PostProdDTO;
using Factory.Domain;

namespace Factory.Application.Interfaces
{
    public interface IShopService
    {
        public List<Product> GetAllProducts();
        
        public Product CreateProduct(ProdDTO dto);

        public Product UpdateProduct(int productID, Product product);
        
        public Product DeleteProduct(int productID);

        public Product GetProductByID(int productID);
    }
}

