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

    public partial class  Fatiecard_DetailController : BaseController
    {
        private readonly  Fatiecard_DetailService  fatiecard_detail;
        public Fatiecard_DetailController( Fatiecard_DetailService _fatiecard_detail)
        {
            fatiecard_detail = _fatiecard_detail;
        }
          /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromBody]Fatiecard_Detail model)
        {
            return fatiecard_detail.AddEntity(model);
        }
          /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Update([FromBody]Fatiecard_Detail model)
        {
            return fatiecard_detail.UpdateEntity(model);
        }
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public Result Delete(string id)
        {
            return fatiecard_detail.DeleteEntity(id);
        }
    }
}




