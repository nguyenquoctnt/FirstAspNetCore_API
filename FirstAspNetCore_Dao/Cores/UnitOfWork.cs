using System;
using System.Data;
using System.Data.SqlClient;
using FirstAspNetCore_Model;

namespace FirstAspNetCore_Dao
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private string _connectionString;
        private bool _disposed;
        
        private IUserRepository _userRepo;

        public UnitOfWork(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new RequiredArgumentBodyException("connection");

            _connectionString = connectionString;
        }
        

        public IUserRepository UserRepository
        {
            get { return _userRepo ?? (_userRepo = new  UserRepository(_transaction, string.Empty)); }
        }
        
        public IUnitOfWork Initialize()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            return this;
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _userRepo = null;
        }

        public void Dispose()
        {
            Disposing(true);
            GC.SuppressFinalize(this);
        }

        private void Disposing(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection.Close();
                    }

                    ResetRepositories();
                }
            }

            _disposed = false;
        }

        ~UnitOfWork()
        {
            Disposing(false);
        }
    }
}
