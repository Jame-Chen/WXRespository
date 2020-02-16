using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reponsitory.Model;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNetCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadFileController : Controller
    {
        private readonly UploadFileService uploadfile;
        public UploadFileController(UploadFileService _uploadfile)
        {
            uploadfile = _uploadfile;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(uploadfile.GetAll());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(uploadfile.GetModelById(id));
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]UploadFile model)
        {
            uploadfile.AddEntity(model);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IActionResult Put([FromBody]UploadFile model)
        {
            uploadfile.UpdateEntity(model);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            uploadfile.DeleteEntity(id);
            return Ok();
        }
    }
}

