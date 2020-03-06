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

    public partial class  UserController : BaseController
    {
        private readonly  UserService  user;
        public UserController( UserService _user)
        {
            user = _user;
        }
          /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromBody]User model)
        {
            return user.AddEntity(model);
        }
          /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Update([FromBody]User model)
        {
            return user.UpdateEntity(model);
        }
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public Result Delete(string id)
        {
            return user.DeleteEntity(id);
        }
    }
}




