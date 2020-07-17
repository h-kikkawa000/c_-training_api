using jp.co.matisse.web.education.controllers.request.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jp.co.matisse.web.education.controllers.response
{
    [Serializable]
    public class Response
    {
        public Response(int _resultCode , object _value)
        {
            this.ResultCode = _resultCode;
            this.Results = _value;
        }
        public int ResultCode { get; }
       // public int ResultCount { get => Results.Count(); }
        public Object Results { get; }

    }
}

