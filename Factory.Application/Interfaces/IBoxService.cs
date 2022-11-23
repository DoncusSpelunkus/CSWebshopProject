using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factory.Application.PostBoxDTO;
using Factory.Core;

namespace Factory.Application.Interfaces
{
    public interface IBoxService
    {
        public List<Box> GetAllBoxes();
        
        public Box insertBox(BoxDTO dto);

        public Box BoxUpdate(int ManFacId, Box box);
        
        public Box BoxDelete(int ManFacId);

        public Box BoxOfIDFinder(int ManFacId);
    }
}

