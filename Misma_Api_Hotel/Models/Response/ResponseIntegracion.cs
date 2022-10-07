using Common.ResponseApi;
using Models.Request;
using System.Collections.Generic;

namespace Models.Response
{
    public class ResponseIntegracion
    {
        public Resultado resultado { get; set; } = new Resultado();
    }

    public class Resultado : ResponseBase
    {
        public List<RequestLogin> datos { get; set; }
    }
}
