﻿using Aritter.Domain.Seedwork;
using Aritter.Domain.Seedwork.Specs;

namespace Aritter.Domain.Security.Aggregates
{
    public interface IUserAccountRepository : IRepository<UserAccount>
    {
        UserAccount Get(string username);
        UserAccount FindUserAuthorizations(ISpecification<UserAccount> specification);
    }
}