
using Sys.Reponsitory.Domain.Model;
using Sys.Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Service
{
    public class RoleService : BaseService<Role>
    {
        public RoleService(IUnitWork UnitWork, IReponsitory<Role> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}

