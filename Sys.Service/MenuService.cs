
using Sys.Reponsitory.Domain.Model;
using Sys.Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Service
{
    public class MenuService : BaseService<Menu>
    {
        public MenuService(IUnitWork UnitWork, IReponsitory<Menu> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}

