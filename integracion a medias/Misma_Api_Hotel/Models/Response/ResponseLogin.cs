using Common.ResponseApi;

namespace Models.Response
{
    //public class ResponseLogin : ResponseBase
    //{
    //    public string Token { get; set; }
    //}

    public class ResponseLogin 
    {
        public ResponseResultado Resultado { get; set; } = new ResponseResultado();
    }

    public class ResponseResultado
    {
        public Datos datos { get; set; } = new Datos();

    }

    public class Datos : ResponseBase
    {
        public string Token { get; set; }
    }




    //// otra manera de hacer la clase sin herencia
    //public class ResponseLogin
    //{       
    //    public string Token { get; set; }
    //    public ResponseBase ResponseL { get; set; }
    //}

}
