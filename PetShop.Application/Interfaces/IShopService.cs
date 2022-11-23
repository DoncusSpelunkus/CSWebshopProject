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
        public List<Product> GetAllBoxes();
        
        public Product insertBox(ProdDTO dto);

        public Product BoxUpdate(int ManFacId, Product box);
        
        public Product BoxDelete(int ManFacId);

        public Product BoxOfIDFinder(int ManFacId);
    }
}

