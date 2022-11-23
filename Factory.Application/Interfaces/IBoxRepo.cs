using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factory.Core;

namespace Factory.Application.Interfaces
{
    public interface IBoxRepo
    {
        public List<Box> GetAllBoxes();

        public Box insertBox(Box box);
        
        public Box BoxUpdate(Box box);

        public Box BoxDelete(int ManFacId);

        public Box BoxOfIDFinder(int ManFacId);
    }
}
