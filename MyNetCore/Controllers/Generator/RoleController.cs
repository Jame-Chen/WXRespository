using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace MyNetCore.Controllers
{

    public partial class  RoleController : BaseController
    {
        private readonly  RoleService  role;
        public RoleController( RoleService _role)
        {
            role = _role;
        }
          /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromBody]Role model)
        {
            return role.AddEntity(model);
        }
          /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Update([FromBody]Role model)
        {
            return role.UpdateEntity(model);
        }
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public Result Delete(string id)
        {
            return role.DeleteEntity(id);
        }
    }
}




