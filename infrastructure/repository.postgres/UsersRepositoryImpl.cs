using IBatisNet.DataMapper;
using jp.co.matisse.web.education.domain.repository.interfaces;
using jp.co.matisse.web.education.infrastructure.repository.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace jp.co.matisse.web.education.infrastructure.repository.postgres
{
    public class UsersRepositoryImpl: IUsersRepository
    {
        public IEnumerable<Users> GetAllUsers()
        {
            try
            {
                return Mapper.Instance().QueryForList<Users>(MethodBase.GetCurrentMethod().Name, null);
            }
            catch(Exception)
            {
                throw;
            }
            
        }

        public Users GetUserId()
        {
            try
            {
                return Mapper.Instance().QueryForObject<Users>(MethodBase.GetCurrentMethod().Name, null);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int AddUser(RegistUserParam _param)
        {
            try
            {
                var test = Mapper.Instance().Insert(MethodBase.GetCurrentMethod().Name, _param);
                return (int)test;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int UpdateUser(RegistUserParam _param)
        {
            try
            {
                return (int)Mapper.Instance().Update(MethodBase.GetCurrentMethod().Name, _param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteUser(DeleteUserParam _param)
        {
            try
            {
                return (int)Mapper.Instance().Update(MethodBase.GetCurrentMethod().Name, _param);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
