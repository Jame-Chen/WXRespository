using Sys.Reponsitory.Domain.Model;
using Sys.Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Service
{
    public class UserService : BaseService<User>
    {
        public UserService(IUnitWork UnitWork, IReponsitory<User> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}
