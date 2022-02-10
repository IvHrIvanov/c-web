using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerSoftUni.HTTP;

namespace WebServerSoftUni.Responses
{
    public class RedirectResponse : Response
    {
        public RedirectResponse(string location)
            : base(StatusCode.Found)
        {
            this.Headers.Add("Location", location);
        }
    }
}
