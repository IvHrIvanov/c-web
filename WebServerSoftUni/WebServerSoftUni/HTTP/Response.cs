using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerSoftUni.HTTP
{
    public class Response
    {
        public StatusCode StatusCode { get; set; }
        public HeaderCollection Headers { get; } = new HeaderCollection();
        public string Body { get; set; }
        public Action<Request, Response> PreRenderAction { get; protected set; }
        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}");

            foreach (var header in Headers)
            {
                result.AppendLine(header.ToString());
            }
            result.AppendLine();

            if (!string.IsNullOrEmpty(Body))
            {
                result.AppendLine(Body);
            }
            return result.ToString();
        }

        public Response(StatusCode statusCode)
        {
            StatusCode = statusCode;
            Headers.Add("Server", "SoftUni Server");
            Headers.Add("Date", $"{DateTime.UtcNow:r}");

        }
    }
}
