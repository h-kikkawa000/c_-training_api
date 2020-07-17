using jp.co.matisse.web.education.infrastructure.repository.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jp.co.matisse.web.education.domain.repository.interfaces
{
    interface IUsersRepository
    {
        IEnumerable<Users> GetAllUsers();

        Users GetUserId();

        int AddUser(RegistUserParam _param);

        int UpdateUser(RegistUserParam _param);

        int DeleteUser(DeleteUserParam _param);
    }
}
