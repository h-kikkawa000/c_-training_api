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
    public class AuthenticationRepositoryImpl: IAuthenticationRepository
    {
        public IEnumerable<Auth> getAuthentication(GetAuthenticationParam _param)
        {
            try
            {
                return Mapper.Instance().QueryForList<Auth>(MethodBase.GetCurrentMethod().Name, _param);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
