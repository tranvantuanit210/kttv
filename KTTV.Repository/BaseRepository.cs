using KTTV.Entities.Repositories;

namespace KTTV.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
    }
}
