
using Model;
using Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class UserService : BaseService<User>
    {
        public UserService(IUnitWork UnitWork, IReponsitory<User> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}
