
using Model;
using Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class RoleService : BaseService<Role>
    {
        public RoleService(IUnitWork UnitWork, IReponsitory<Role> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}
