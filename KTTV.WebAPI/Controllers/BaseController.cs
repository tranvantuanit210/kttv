using System;
using System.Linq;
using System.Web.Http;

namespace KTTV.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        protected string GenerationAccessToken()
        {
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());

            return token;
        }
    }
}
