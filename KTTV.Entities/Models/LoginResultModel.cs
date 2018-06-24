using System;

namespace KTTV.Entities.Models
{
    public class LoginResultModel
    {
        public UserModel User { get; set; }
        public string AccessToken { get; set; }
        public DateTime TokenExpired { get; set; }
    }
}
