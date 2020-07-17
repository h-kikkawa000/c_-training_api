using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace jp.co.matisse.web.education.controllers.request.param
{
    /// <summary>
    /// IsAuthenticationメソッドリクエストパラメーターモデルクラス
    /// </summary>
    [JsonObject]
    public class IsAuthenticationModel
    {
        /// <summary>
        /// ユーザー名プロパティ（必須）
        /// </summary>
        [JsonProperty("user_name")]
        [Required]
        public string UserName { set; get; }

        /// <summary>
        /// パスワードプロパティ（必須）
        /// </summary>
        [JsonProperty("user_pass")]
        [Required]
        public string UserPass { set; get; }
    }
}
