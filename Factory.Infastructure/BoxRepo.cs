using Factory.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factory.Application.Interfaces;

namespace Factory.Infastructure
{
    public class BoxRepo : IBoxRepo
    {
        private BoxDbContext _boxDbContext;

        public BoxRepo(BoxDbContext boxDbContext)
        {
            _boxDbContext = boxDbContext;
        }

        public List<Box> GetAllBoxes()
        {
            return _boxDbContext.BoxTable.ToList();
        }

        public Box insertBox(Box box)
        {
            _boxDbContext.BoxTable.Add(box);
            _boxDbContext.SaveChanges();
            return box;
        }

        public Box BoxUpdate(Box box)
        {
            _boxDbContext.BoxTable.Update(box);
            _boxDbContext.SaveChanges();
            return box;
        }
        
        public Box BoxDelete(int ManFacId)
        {
            Box box = BoxOfIDFinder(ManFacId);
            _boxDbContext.BoxTable.Remove(box);
            _boxDbContext.SaveChanges();
            return box;
        }

        public Box BoxOfIDFinder(int ManFacId)
        {
            return _boxDbContext.BoxTable.FirstOrDefault(p => p.ManFacId == ManFacId);
        }

        public void CreateDB()
        {
            _boxDbContext.Database.EnsureDeleted();
            _boxDbContext.Database.EnsureCreated();
        }
    }
}
