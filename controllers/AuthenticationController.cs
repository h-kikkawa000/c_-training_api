using jp.co.matisse.web.education.controllers.request.param;
using jp.co.matisse.web.education.domain.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace jp.co.matisse.web.education.controllers
{
    /// <summary>
    /// ユーザー認証用コントローラークラス
    /// </summary>
    [Route("api/[controller]")]
    public class AuthenticationController : ApiBaseController
    {
        /// <summary>
        /// 認証実施メソッド
        /// </summary>
        /// <param name="_requestPram">リクエストパラメーター</param>
        /// <returns>レスポンスパラメーター</returns>
        [HttpPost("IsAuthentication")]
        public IActionResult IsAuthentication([FromBody]IsAuthenticationModel _requestPram)
        {
            try{
                IAuthenticationService authenticationService = new AuthenticationServiceImpl();
                ResultAuthenticationService result = authenticationService.isAuthentication(_requestPram);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }



}