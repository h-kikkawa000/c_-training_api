using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jp.co.matisse.web.education.infrastructure.repository.entity
{
    [Serializable]
    public class Users
    {
        public int Id { set; get; }

        public string UserId { set; get; }

        public string UserPass { set; get; }

        public int StaffKbn { set; get; }

        public string StaffDesc { set; get; }

        public string StoreCode { set; get; }
    }

    [Serializable]
    public class RegistUserParam
    {
        public int Id { set; get; }

        public string UserId { set; get; }

        public string UserPass { set; get; }

        public int StaffKbn { set; get; }

        public string StoreCode { set; get; }
    }

    [Serializable]
    public class DeleteUserParam
    {
        public int Id { set; get; }

    }
}
