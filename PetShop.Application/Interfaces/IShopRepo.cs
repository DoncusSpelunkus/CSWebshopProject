using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factory.Domain;

namespace Factory.Application.Interfaces
{
    public interface IShopRepo
    {
        public List<Product> GetAllBoxes();

        public Product insertBox(Product box);
        
        public Product BoxUpdate(Product box);

        public Product BoxDelete(int ManFacId);

        public Product BoxOfIDFinder(int ManFacId);
    }
}
