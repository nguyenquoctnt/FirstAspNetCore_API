using System;

namespace FirstAspNetCore_Model
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        void Commit();
        IUnitOfWork Initialize();
    }
}