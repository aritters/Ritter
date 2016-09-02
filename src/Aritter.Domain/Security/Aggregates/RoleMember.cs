﻿using Aritter.Domain.Seedwork;

namespace Aritter.Domain.Security.Aggregates
{
    public class RoleMember : Entity
    {
        public RoleMember(Role role, UserAccount member)
           : this()
        {
            Role = role;
            RoleId = role.Id;

            Member = member;
            MemberId = member.Id;
        }

        private RoleMember()
            : base()
        {
        }

        public int RoleId { get; private set; }

        public int MemberId { get; private set; }

        public virtual Role Role { get; private set; }

        public virtual UserAccount Member { get; private set; }

        public static RoleMember CreateAssignment(Role role, UserAccount member)
        {
            return new RoleMember(role, member);
        }
    }
}