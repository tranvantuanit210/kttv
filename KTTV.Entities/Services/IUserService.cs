using KTTV.Entities.Models;

namespace KTTV.Entities.Services
{
    public interface IUserService : IBaseService<NguoiDung>
    {
        LoginResultModel Login(UserModel user);
    }
}
