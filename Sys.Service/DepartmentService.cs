
using Sys.Reponsitory.Domain.Model;
using Sys.Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Service
{
    public class DepartmentService : BaseService<Department>
    {
        public DepartmentService(IUnitWork UnitWork, IReponsitory<Department> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}

