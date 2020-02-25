using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Result
    {
        public Result()
        {
            Code = "200";
            Msg = "操作成功!";
        }
        public string Code { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
    }
}
