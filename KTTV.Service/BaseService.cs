using KTTV.Entities.Services;

namespace KTTV.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
    }
}
