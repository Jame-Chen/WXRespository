using Reponsitory.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace  Service
{
    public partial class Fatiecard_DetailService : BaseService<Fatiecard_Detail>
    {
        public Fatiecard_DetailService(IUnitWork UnitWork, IReponsitory<Fatiecard_Detail> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}
