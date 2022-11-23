using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factory.Application.Interfaces;
using Factory.Domain;

namespace Factory.Infastructure
{
    public class ShopRepo : IShopRepo
    {
        private ShopDbContext _boxDbContext;

        public ShopRepo(ShopDbContext boxDbContext)
        {
            _boxDbContext = boxDbContext;
        }

        public List<Product> GetAllBoxes()
        {
            return _boxDbContext.BoxTable.ToList();
        }

        public Product insertBox(Product box)
        {
            _boxDbContext.BoxTable.Add(box);
            _boxDbContext.SaveChanges();
            return box;
        }

        public Product BoxUpdate(Product box)
        {
            _boxDbContext.BoxTable.Update(box);
            _boxDbContext.SaveChanges();
            return box;
        }
        
        public Product BoxDelete(int ManFacId)
        {
            Product box = BoxOfIDFinder(ManFacId);
            _boxDbContext.BoxTable.Remove(box);
            _boxDbContext.SaveChanges();
            return box;
        }

        public Product BoxOfIDFinder(int ManFacId)
        {
            return _boxDbContext.BoxTable.FirstOrDefault(p => p.ID == ManFacId);
        }

        public void CreateDB()
        {
            _boxDbContext.Database.EnsureDeleted();
            _boxDbContext.Database.EnsureCreated();
        }
    }
}
