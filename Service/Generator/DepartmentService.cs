using Reponsitory.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace  Service
{
    public partial class DepartmentService : BaseService<Department>
    {
        public DepartmentService(IUnitWork UnitWork, IReponsitory<Department> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}
