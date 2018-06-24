using System;
using System.Collections.Generic;
using System.Web.Http;
using KTTV.Entities.Models;
using KTTV.Entities.Services;
using KTTV.Service;

namespace KTTV.WebAPI.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/authentication")]
    public class AuthenticationController : BaseController
    {
        private readonly IUserService _userService;
        public AuthenticationController()
        {
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("login")]
        public LoginResultModel Post([FromBody]UserModel user)
        {
            try
            {
                if (user == null)
                    throw new Exception("Bad request");

                var userLogin = _userService.Login(user);
                userLogin.AccessToken = GenerationAccessToken();

                return userLogin;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}