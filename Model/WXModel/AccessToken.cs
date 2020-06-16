using System;
using System.Collections.Generic;
using System.Text;

namespace Model.WXModel
{
  public   class AccessToken: WXBase
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
       
    }
}
