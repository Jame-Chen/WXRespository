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

    public partial class  RoleController : BaseController
    {
        private readonly  RoleService  role;
        public RoleController( RoleService _role)
        {
            role = _role;
        }

        [HttpPost]
        public Result Add([FromBody]Role model)
        {
            return role.AddEntity(model);
        }

        [HttpPost]
        public Result Update([FromBody]Role model)
        {
            return role.UpdateEntity(model);
        }

        [HttpPost("{id}")]
        public Result Delete(string id)
        {
            return role.DeleteEntity(id);
        }
    }
}




