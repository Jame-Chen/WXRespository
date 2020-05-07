using Reponsitory.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace  Service
{
    public partial class Article_StatusService : BaseService<Article_Status>
    {
        public Article_StatusService(IUnitWork UnitWork, IReponsitory<Article_Status> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}
