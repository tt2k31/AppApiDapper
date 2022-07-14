﻿using AppApiDapper.Services.Repository;

namespace AppApiDapper.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IOrganizationRepository OrganizationRepository { get; }
        IManagerListRepository ManagerListRepository { get; }
        IMembershipRepository MembershipRepository { get; }

        void Commit();
    }
}