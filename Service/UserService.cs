using Reponsitory.Model;
using Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Service
{
    public class UserService : BaseService<User>
    {
        public UserService(IUnitWork UnitWork, IReponsitory<User> reponsitory) : base(UnitWork, reponsitory)
        {

        }

        public object Test()
        {
            var user = Reponsitory.Find(f => true);
            return user;
        }
    }
}
