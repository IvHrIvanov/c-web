﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerSoftUni.HTTP;

namespace WebServerSoftUni.Responses
{
    public class NotFoundResponse : Response
    {
        public NotFoundResponse() :
            base(StatusCode.NotFound)
        {
        }
    }
}