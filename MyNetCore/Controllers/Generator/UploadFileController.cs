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

    public partial class  UploadFileController : BaseController
    {
        private readonly  UploadFileService  uploadfile;
        public UploadFileController( UploadFileService _uploadfile)
        {
            uploadfile = _uploadfile;
        }
          /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromBody]UploadFile model)
        {
            return uploadfile.AddEntity(model);
        }
          /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Update([FromBody]UploadFile model)
        {
            return uploadfile.UpdateEntity(model);
        }
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public Result Delete(string id)
        {
            return uploadfile.DeleteEntity(id);
        }
    }
}




