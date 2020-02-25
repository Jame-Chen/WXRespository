
using Model;
using Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class DepartmentService : BaseService<Department>
    {
        public DepartmentService(IUnitWork UnitWork, IReponsitory<Department> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}

