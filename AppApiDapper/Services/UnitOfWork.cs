using AppApiDapper.Services.Repository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AppApiDapper.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        private IManagerListRepository _managerListRepository;
        private IMembershipRepository _membershipRepository;
        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IOrganizationRepository OrganizationRepository => throw new NotImplementedException();

        public IManagerListRepository ManagerListRepository
        {
            get
            {
                return _managerListRepository ??
                    (_managerListRepository = new ManagerListRepository(_transaction));
            }
        }
        public IMembershipRepository MembershipRepository
        {
            get
            {
                return _membershipRepository ??
                    (_membershipRepository = new MembershipRepository(_transaction));
            }
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
                resetRepositories();
            }
        }
        private void resetRepositories()
        {
            _managerListRepository = null;
            
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }
        private void dispose(bool disposing)
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
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
