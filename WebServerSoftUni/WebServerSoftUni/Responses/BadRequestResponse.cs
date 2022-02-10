using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerSoftUni.HTTP;

namespace WebServerSoftUni.Responses
{
    public class BadRequestResponse : Response
    {
        public BadRequestResponse() 
            : base(StatusCode.BadRequest)
        {
        }
    }
}
