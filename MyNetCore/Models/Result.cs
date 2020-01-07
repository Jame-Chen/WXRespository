using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore.Models
{
    public class Result
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
    }
}
