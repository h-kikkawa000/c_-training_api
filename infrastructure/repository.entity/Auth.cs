using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jp.co.matisse.web.education.infrastructure.repository.entity
{
    public class Auth
    {
        public string UserName { set; get; }
        public int StaffKbn { set; get; }

        public string StaffDesc { set; get; }

        public string StoreCode { set; get; }
    }


    public class GetAuthenticationParam
    {
        public string UserId { set; get; }

        public string UserPass { set; get; }
    }
}
