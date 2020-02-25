using Model;
using Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class MenuService : BaseService<Menu>
    {
        public MenuService(IUnitWork UnitWork, IReponsitory<Menu> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}

