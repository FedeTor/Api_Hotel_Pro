using Common.ResponseApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Response
{
    public class ResponseLogin : ResponseBase
    {
        public string Token { get; set; }
    }
}
