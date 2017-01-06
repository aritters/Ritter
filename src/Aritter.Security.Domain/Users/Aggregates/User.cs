﻿using Aritter.Domain.Seedwork;

namespace Aritter.Security.Domain.Users.Aggregates
{
    public class User : Entity
    {
        public string Username { get; set; }
        public Credential Credential { get; set; }
    }
}