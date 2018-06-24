using KTTV.Entities;
using KTTV.Entities.Models;
using KTTV.Entities.Repositories;
using KTTV.Entities.Services;

namespace KTTV.Service
{
    public class UserService : BaseService<NguoiDung>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public LoginResultModel Login(UserModel user)
        {
            return _userRepository.Login(user);
        }
    }
}
