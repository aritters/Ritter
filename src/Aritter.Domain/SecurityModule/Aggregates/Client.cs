﻿using Aritter.Domain.Seedwork;
using System.Collections.Generic;

namespace Aritter.Domain.SecurityModule.Aggregates
{
    public class Client : Entity
    {
        public Client(string name)
            : this()
        {
            Name = name;
        }

        public Client(string name, string description)
            : this(name)
        {
            Description = description;
        }

        private Client()
            : base()
        {
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public virtual ICollection<Rule> Rules { get; private set; } = new List<Rule>();

        public virtual ICollection<Resource> Resources { get; private set; } = new List<Resource>();

        public virtual ICollection<Role> UserRoles { get; private set; } = new List<Role>();
    }
}
