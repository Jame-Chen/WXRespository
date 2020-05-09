using Reponsitory.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace  Service
{
    public partial class User_InfoService : BaseService<User_Info>
    {
        public User_InfoService(IUnitWork UnitWork, IReponsitory<User_Info> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}
