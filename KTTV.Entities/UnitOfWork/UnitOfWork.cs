using System;
using KTTV.Entities.Repositories;

namespace KTTV.Entities.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IUserRepository UserRepository { get; }
        public void SaveChages()
        {
            throw new NotImplementedException();
        }
    }
}