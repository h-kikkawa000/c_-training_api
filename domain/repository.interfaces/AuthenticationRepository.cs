using jp.co.matisse.web.education.infrastructure.repository.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jp.co.matisse.web.education.domain.repository.interfaces
{
    interface IAuthenticationRepository
    {
        IEnumerable<Auth> getAuthentication(GetAuthenticationParam _param);
    }
}
