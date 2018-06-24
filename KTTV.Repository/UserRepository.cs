using System.Data;
using KTTV.Entities;
using KTTV.Entities.Models;
using KTTV.Entities.Repositories;
using KTTV.Entities.Utilities;

namespace KTTV.Repository
{
    public class UserRepository : IBaseRepository<NguoiDung>, IUserRepository
    {
        public LoginResultModel Login(UserModel user)
        {
            var login = new LoginResultModel()
            {
                User = user
            };

            return login;
        }
    }
}
