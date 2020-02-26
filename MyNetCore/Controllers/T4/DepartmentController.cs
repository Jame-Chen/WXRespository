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

    public partial class  DepartmentController : BaseController
    {
        private readonly  DepartmentService  department;
        public DepartmentController( DepartmentService _department)
        {
            department = _department;
        }

        [HttpPost]
        public Result Add([FromBody]Department model)
        {
            return department.AddEntity(model);
        }

        [HttpPost]
        public Result Update([FromBody]Department model)
        {
            return department.UpdateEntity(model);
        }

        [HttpPost("{id}")]
        public Result Delete(string id)
        {
            return department.DeleteEntity(id);
        }
    }
}




