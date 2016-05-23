using System;

namespace Aritter.Domain.Seedwork
{
    public abstract class Entity : IEntity
    {
        #region Members

        private int? requestedHashCode;

        #endregion

        #region Properties

        public virtual int Id { get; protected set; }

        public bool IsEnabled { get; protected set; }

        public virtual Guid Identity { get; protected set; }

        #endregion

        #region Public Methods

        public bool IsTransient()
        {
            return Identity == Guid.Empty;
        }

        public bool IsStored()
        {
            return Id > default(int);
        }

        public void GenerateIdentity()
        {
            if (IsTransient())
            {
                Identity = Guid.NewGuid();
            }
        }

        public void ChangeCurrentIdentity(Guid identity)
        {
            if (!IsTransient())
            {
                Identity = identity;
            }
        }

        public void Disable()
        {
            if (IsEnabled)
            {
                IsEnabled = false;
            }
        }

        public void Enable()
        {
            if (!IsEnabled)
            {
                IsEnabled = true;
            }
        }

        #endregion

        #region Overrides Methods

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var item = (Entity)obj;

            if (item.IsTransient() || IsTransient())
            {
                return false;
            }

            return item.Id == Id && item.Identity == Identity;
        }

        public override int GetHashCode()
        {
            if (IsTransient())
            {
                return base.GetHashCode();
            }

            if (!requestedHashCode.HasValue)
            {
                requestedHashCode = Identity.GetHashCode() ^ 31;
            }

            return requestedHashCode.Value;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, null)
                ? Equals(right, null)
                : left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        #endregion
    }
}