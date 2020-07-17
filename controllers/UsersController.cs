using System;
using jp.co.matisse.web.education.controllers.request.param;
using jp.co.matisse.web.education.controllers.response;
using jp.co.matisse.web.education.domain.service;
using jp.co.matisse.web.education.infrastructure.repository.entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jp.co.matisse.web.education.controllers
{
    /// <summary>
    /// ユーザー情報管理用コントローラークラス
    /// </summary>
    [Route("api/[controller]")]
    public class UsersController : ApiBaseController
    {
        /// <summary>
        /// ユーザー情報取得メソッド
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                IUserService usersServiceImpl = new UsersServiceImpl();
                ResultGetUser result = usersServiceImpl.GetUser();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(1,ex));
            }
        }

        /// <summary>
        /// ユーザー情報ID取得メソッド
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetID")]
        public IActionResult GetID()
        {
            try
            {
                IUserService usersServiceImpl = new UsersServiceImpl();
                int result = usersServiceImpl.GetUserId();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        /// <summary>
        /// ユーザー情報登録メソッド
        /// </summary>
        /// <param name="_requestPram">リクエストパラメーター</param>
        /// <returns>レスポンスパラメーター</returns>
        /// <remarks>ユーザー情報の新規登録を実施</remarks>
        [HttpPost("Register")]
        public IActionResult Register([FromBody]UsersRegisterModel _requestPram)
        {
            try
            {
                IUserService usersServiceImpl = new UsersServiceImpl();
                int result = usersServiceImpl.AddUser(_requestPram);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        /// <summary>
        /// ユーザー情報更新メソッド
        /// </summary>
        /// <param name="_requestPram">リクエストパラメーター</param>
        /// <returns>レスポンスパラメーター</returns>
        /// <remarks>ユーザー情報の更新を実施</remarks>
        [HttpPost("Update")]
        public IActionResult Update([FromBody]UsersRegisterModel _requestPram)
        {
            try
            {
                IUserService usersServiceImpl = new UsersServiceImpl();
                int result = usersServiceImpl.UpdateUser(_requestPram);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        /// <summary>
        /// ユーザー情報削除メソッド
        /// </summary>
        /// <param name="_requestPram">リクエストパラメーター</param>
        /// <returns>レスポンスパラメーター</returns>
        /// <remarks>ユーザー情報の削除を実施</remarks>
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] UsersDeleteModel _requestPram)
        {
            try
            {
                IUserService usersServiceImpl = new UsersServiceImpl();
                int result = usersServiceImpl.DeleteUser(_requestPram);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }


}