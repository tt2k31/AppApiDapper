﻿using System.Data;

namespace AppApiDapper.Services.Repository
{
    public abstract class RepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { 
            get
            { 
                return Transaction.Connection; 
            } 
        }
        public RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
