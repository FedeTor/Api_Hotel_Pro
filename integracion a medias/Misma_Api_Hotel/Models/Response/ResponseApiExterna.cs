using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Response
{
    public class ResponseApiExterna
    {
        public DatosInteresan data { get; set; }
        public bool success { get; set; }
        public int  status { get; set; }

    }

    public class DatosInteresan
    {
        public string id { get; set; }
        public string display_url { get; set; }
    }
}
