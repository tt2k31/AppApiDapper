using AppApiDapper.Models;
using AppApiDapper.Services.Interface;
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
        private readonly MyDBContext _context;
        
        

        private IOrganizationRepository _organizationRepository;
        private IManagerListRepository _managerListRepository;
        private IMembershipRepository _membershipRepository;
        private IUserRepository _userRepository;
        

        public UnitOfWork(IConfiguration config)
        {
            
            _connection = new SqlConnection(config.GetConnectionString("MyDb"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IOrganizationRepository OrganizationRepository
        {
            get
            {
                return _organizationRepository ??
                    (_organizationRepository = new OrganizationRepository(_transaction));
            }
        }

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
        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ??
                    (_userRepository = new UserRepository(_transaction));
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
