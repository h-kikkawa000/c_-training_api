using jp.co.matisse.web.education.controllers.request.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jp.co.matisse.web.education.domain.service
{
    interface IUserService
    {
        ResultGetUser GetUser();

        int GetUserId();

        int AddUser(UsersRegisterModel _param);

        int UpdateUser(UsersRegisterModel _param);

        int DeleteUser(UsersDeleteModel _param);
    }
}
