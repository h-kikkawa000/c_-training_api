using jp.co.matisse.web.education.controllers.request.param;
using jp.co.matisse.web.education.domain.repository.interfaces;
using jp.co.matisse.web.education.infrastructure.repository.entity;
using jp.co.matisse.web.education.infrastructure.repository.postgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jp.co.matisse.web.education.domain.service
{
    public class UsersServiceImpl: IUserService
    {
        public ResultGetUser GetUser()
        {
            // DB検索実施
            IUsersRepository authenticationRepository = new UsersRepositoryImpl();
            var result = authenticationRepository.GetAllUsers();
            return new ResultGetUser(result);
        }

        public int GetUserId()
        {
            // DB検索実施
            IUsersRepository authenticationRepository = new UsersRepositoryImpl();
            Users result = authenticationRepository.GetUserId();
            return result.Id;
        }


        public int AddUser(UsersRegisterModel _param)
        {
            // DB検索用パラメータークラス用意
            RegistUserParam registUserParam = new RegistUserParam();
            registUserParam.UserId = _param.UserId;
            registUserParam.UserPass = _param.UserPass;
            registUserParam.StaffKbn = _param.StaffKbn;
            registUserParam.StoreCode = _param.StoreCode;

            // DB検索実施
            IUsersRepository authenticationRepository = new UsersRepositoryImpl();
            var result = authenticationRepository.AddUser(registUserParam);
            return result;
        }

        public int UpdateUser(UsersRegisterModel _param)
        {
            // DB検索用パラメータークラス用意
            RegistUserParam registUserParam = new RegistUserParam();
            registUserParam.Id = _param.Id;
            registUserParam.UserId = _param.UserId;
            registUserParam.UserPass = _param.UserPass;
            registUserParam.StaffKbn = _param.StaffKbn;
            registUserParam.StoreCode = _param.StoreCode;

            // DB検索実施
            IUsersRepository authenticationRepository = new UsersRepositoryImpl();
            var result = authenticationRepository.UpdateUser(registUserParam);
            return result;
        }

        public int DeleteUser(UsersDeleteModel _param)
        {
            // DB検索用パラメータークラス用意
            DeleteUserParam DeleteUserParam = new DeleteUserParam();
            DeleteUserParam.Id = _param.Id;

            // 削除実施
            IUsersRepository authenticationRepository = new UsersRepositoryImpl();
            var result = authenticationRepository.DeleteUser(DeleteUserParam);
            return result;
        }
    }

    public class ResultGetUser
    {
        private IEnumerable<Users> result;


        public ResultGetUser(IEnumerable<Users> _value)
        {
            this.result = _value;
        }

        public IEnumerable<Users> Result
        {
            get { return this.result; }
        }
    }
}
