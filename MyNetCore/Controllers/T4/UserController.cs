using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNetCore.Controllers
{

    public partial class  UserController : BaseController
    {
        private readonly  UserService  user;
        public UserController( UserService _user)
        {
            user = _user;
        }

        [HttpPost]
        public Result Add([FromBody]User model)
        {
            return user.AddEntity(model);
        }

        [HttpPost]
        public Result Update([FromBody]User model)
        {
            return user.UpdateEntity(model);
        }

        [HttpPost("{id}")]
        public Result Delete(string id)
        {
            return user.DeleteEntity(id);
        }
    }
}




