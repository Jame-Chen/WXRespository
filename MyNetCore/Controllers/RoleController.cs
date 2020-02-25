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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly RoleService role;
        public RoleController(RoleService _role)
        {
            role = _role;
        }
      

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Role model)
        {
            role.AddEntity(model);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IActionResult Put([FromBody]Role model)
        {
            role.UpdateEntity(model);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            role.DeleteEntity(id);
            return Ok();
        }
    }
}

