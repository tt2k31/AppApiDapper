﻿using AppApiDapper.Services.Repository;
using AppApiDapper.Services.Interface;

namespace AppApiDapper.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IOrganizationRepository OrganizationRepository { get; }
        IManagerListRepository ManagerListRepository { get; }
        IMembershipRepository MembershipRepository { get; }
        ILogin Login { get; }
        
        Task Commit();
        Task CompleteAsync();
    }
}
