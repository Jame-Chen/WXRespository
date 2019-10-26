using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sys.Reponsitory.Domain.Model;
using Sys.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNetCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DepartmentService department;
        public DepartmentController(DepartmentService _department)
        {
            department = _department;
        }
        [HttpGet]
        public IActionResult Get()
        {
            //var data = HttpContext.User;//获取jwt数据
            return Ok(department.GetAll());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(department.GetModelById(id));
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Department model)
        {
            department.AddEntity(model);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IActionResult Put([FromBody]Department model)
        {
            department.UpdateEntity(model);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            department.DeleteEntity(id);
            return Ok();
        }
    }
}

