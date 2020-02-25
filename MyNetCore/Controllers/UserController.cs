﻿using System;
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
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly UserService user;
        public UserController(UserService _user)
        {
            user = _user;
        }
       

   

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]User model)
        {
            user.AddEntity(model);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IActionResult Put([FromBody]User model)
        {
            user.UpdateEntity(model);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            user.DeleteEntity(id);
            return Ok();
        }
    }
}

