using KTTV.Entities.Models;

namespace KTTV.Entities.Repositories
{
    public interface IUserRepository: IBaseRepository<NguoiDung>
    {
        LoginResultModel Login(UserModel user);
    }
}
