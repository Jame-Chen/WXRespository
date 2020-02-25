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
    public class MenuController : Controller
    {
        private readonly MenuService menu;
        public MenuController(MenuService _menu)
        {
            menu = _menu;
        }
      

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Menu model)
        {
            menu.AddEntity(model);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IActionResult Put([FromBody]Menu model)
        {
            menu.UpdateEntity(model);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            menu.DeleteEntity(id);
            return Ok();
        }
    }
}

