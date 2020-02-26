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

    public partial class  UploadFileController : BaseController
    {
        private readonly  UploadFileService  uploadfile;
        public UploadFileController( UploadFileService _uploadfile)
        {
            uploadfile = _uploadfile;
        }

        [HttpPost]
        public Result Add([FromBody]UploadFile model)
        {
            return uploadfile.AddEntity(model);
        }

        [HttpPost]
        public Result Update([FromBody]UploadFile model)
        {
            return uploadfile.UpdateEntity(model);
        }

        [HttpPost("{id}")]
        public Result Delete(string id)
        {
            return uploadfile.DeleteEntity(id);
        }
    }
}




