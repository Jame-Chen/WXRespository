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

    public partial class  MenuController : BaseController
    {
        private readonly  MenuService  menu;
        public MenuController( MenuService _menu)
        {
            menu = _menu;
        }

        [HttpPost]
        public Result Add([FromBody]Menu model)
        {
            return menu.AddEntity(model);
        }

        [HttpPost]
        public Result Update([FromBody]Menu model)
        {
            return menu.UpdateEntity(model);
        }

        [HttpPost("{id}")]
        public Result Delete(string id)
        {
            return menu.DeleteEntity(id);
        }
    }
}




