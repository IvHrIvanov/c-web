using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerSoftUni.Common;

namespace WebServerSoftUni.HTTP
{
    public class ContentResponse : Response
    {
        public ContentResponse(string content,string contentType,Action<Request,Response> preRenderAction = null) 
            : base(StatusCode.OK)
        {
            Guard.AgainstNull(content);
            Guard.AgainstNull(contentType);
            this.Headers.Add(Header.ContentType, contentType);
            this.Body = content;
            this.PreRenderAction = preRenderAction;
        }
        public override string ToString()
        {
            if (Body != null)
            {
                var contentLenght = Encoding.UTF8.GetByteCount(Body).ToString();
                //this.Headers.Add(Header.ContentLenght, contentLenght);
            }
            return base.ToString();
        }
    }
}
