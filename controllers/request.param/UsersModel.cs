using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace jp.co.matisse.web.education.controllers.request.param
{
    /// <summary>
    /// Registerメソッドリクエストパラメーターモデルクラス
    /// </summary>
    [JsonObject]
    public class UsersRegisterModel
    {
        /// <summary>
        /// ユーザーIDプロパティ（必須）
        /// </summary>
        [JsonProperty("id")]
        [Required]
        [Key]
        public int Id { set; get; }

        /// <summary>
        /// ユーザー名プロパティ（必須）
        /// </summary>
        [JsonProperty("user_id")]
        [Required]
        [StringLength(5)]
        public string UserId { set; get; }

        /// <summary>
        /// パスワードプロパティ（必須）
        /// </summary>
        [JsonProperty("user_pass")]
        [Required]
        [StringLength(10)]
        public string UserPass { set; get; }

        /// <summary>
        /// ストア区分プロパティ（必須）
        /// </summary>
        /// <remarks>０：店舗ユーザー １：商品部ユーザー</remarks>
        [JsonProperty("staff_kbn")]
        [Required]
        [Range(0,1)]
        public int StaffKbn { set; get; }

        /// <summary>
        /// ストア区分プロパティ
        /// </summary>
        /// <remarks>店舗ユーザーの場合のみ使用</remarks>
        [JsonProperty("store_code")]
        [StringLength(4)]
        public string StoreCode { set; get; }
    }

    /// <summary>
    /// DeleteRegisterメソッドリクエストパラメーターモデルクラス
    /// </summary>
    [JsonObject]
    public class UsersDeleteModel
    {
        /// <summary>
        /// ユーザーIDプロパティ（必須）
        /// </summary>
        [JsonProperty("id")]
        [Required]
        [Key]
        public int Id { set; get; }
    }
}
