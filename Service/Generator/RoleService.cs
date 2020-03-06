using Reponsitory.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace  Service
{
    public partial class RoleService : BaseService<Role>
    {
        public RoleService(IUnitWork UnitWork, IReponsitory<Role> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}
