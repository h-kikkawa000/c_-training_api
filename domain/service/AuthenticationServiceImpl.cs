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
    public class AuthenticationServiceImpl: IAuthenticationService
    {
        public AuthenticationServiceImpl()
        {
        }

        public ResultAuthenticationService isAuthentication(IsAuthenticationModel _param)
        {
            // DB検索用パラメータークラス用意
            GetAuthenticationParam getAuthenticationParam = new GetAuthenticationParam();
            getAuthenticationParam.UserId = _param.UserName;
            getAuthenticationParam.UserPass = _param.UserPass;

            // DB検索実施
            IAuthenticationRepository authenticationRepository = new AuthenticationRepositoryImpl();
            var result = authenticationRepository.getAuthentication(getAuthenticationParam);
            return new ResultAuthenticationService(result);
        }

    }

    public class ResultAuthenticationService
    {
        private IEnumerable<Auth> result;


        public ResultAuthenticationService(IEnumerable<Auth> _value)
        {
            this.result = _value;
        }

        public IEnumerable<Auth> Result
        {
            get { return this.result; }
        }
    }
}
