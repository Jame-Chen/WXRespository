using Reponsitory.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace  Service
{
    public partial class UserService : BaseService<User>
    {
        public UserService(IUnitWork UnitWork, IReponsitory<User> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}
