﻿using Aritter.Domain.Seedwork;
using Aritter.Domain.Seedwork.Specs;
using System.Collections.Generic;

namespace Aritter.Domain.SecurityModule.Aggregates.Permissions
{
    public interface IAuthorizationRepository : IRepository<Authorization>
    {
        ICollection<Authorization> ListAuthorizations(ISpecification<Authorization> specification);
    }
}