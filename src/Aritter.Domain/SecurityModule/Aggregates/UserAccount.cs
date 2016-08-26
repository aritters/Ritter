using Aritter.Domain.Seedwork;
using Aritter.Infra.Crosscutting.Encryption;
using Aritter.Infra.Crosscutting.Exceptions;
using System.Collections.Generic;

namespace Aritter.Domain.SecurityModule.Aggregates
{
    public class UserAccount : Entity
    {
        public UserAccount(string username, string email)
            : this()
        {
            Username = username;
            Email = email;
        }

        private UserAccount()
            : base()
        {
        }

        public string Username { get; private set; }

        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool MustChangePassword { get; private set; }
        public int InvalidLoginAttemptsCount { get; private set; }
        public int? UserProfileId { get; private set; }

        public virtual UserProfile UserProfile { get; private set; }
        public virtual ICollection<UserAssignment> Assignments { get; private set; } = new List<UserAssignment>();

        #region Methods

        public void ChangePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ApplicationException("Invalid password");
            }

            Password = Encrypter.Encrypt(password);
        }

        public void SetProfile(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                ThrowHelper.ThrowApplicationException("Invalid user profile");
            }

            UserProfile = userProfile;
            UserProfileId = userProfile.Id;
        }

        public void HasInvalidLoginAttempt()
        {
            InvalidLoginAttemptsCount++;
        }

        public void HasValidLoginAttempt()
        {
            InvalidLoginAttemptsCount = 0;
        }

        #endregion
    }
}
