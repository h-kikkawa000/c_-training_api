using jp.co.matisse.web.education.controllers.filters;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace jp.co.matisse.web.education.controllers
{
    /// <summary>
    /// webApi基底クラス
    /// </summary>
    [ApiController]
    [ValidateModel]
    public class ApiBaseController : ControllerBase
    {
    }
}