using jp.co.matisse.web.education.controllers.request.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jp.co.matisse.web.education.domain.service
{
    interface IAuthenticationService
    {
        ResultAuthenticationService isAuthentication(IsAuthenticationModel _param);

    }
}
