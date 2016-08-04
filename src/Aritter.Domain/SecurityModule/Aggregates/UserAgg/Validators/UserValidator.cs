﻿using Aritter.Domain.SecurityModule.Aggregates.Users.Specs;
using Aritter.Domain.Seedwork.Rules.Validation;

namespace Aritter.Domain.SecurityModule.Aggregates.Users.Validators
{
    public sealed class UserValidator : EntityValidator<User>
    {
        public const string CredentialsValidation = "CredentialsValidation";
        public const string UserIsNotNullValidation = "UserIsNotNullValidation";

        public ValidationResult ValidateCredentials(User user, string password)
        {
            RemoveValidations();

            var spec = new ValidUserCredentialSpec(password);
            AddValidation(CredentialsValidation, new ValidationRule<User>(spec, "Invalid username or password."));

            return Validate(user, CredentialsValidation);
        }

        public ValidationResult ValidateUser(User user)
        {
            RemoveValidations();

            var spec = new ValidUserSpec();
            AddValidation(UserIsNotNullValidation, new ValidationRule<User>(spec, "Invalid username or password."));

            return Validate(user, UserIsNotNullValidation);
        }
    }
}
