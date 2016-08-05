﻿using Aritter.Application.DTO.SecurityModule.Authentication;
using Aritter.Application.DTO.SecurityModule.Users;
using Aritter.Application.Seedwork.Extensions;
using Aritter.Application.Seedwork.Services;
using Aritter.Application.Seedwork.Services.SecurityModule;
using Aritter.Domain.SecurityModule.Aggregates.Users;
using Aritter.Domain.SecurityModule.Aggregates.Users.Specs;
using Aritter.Domain.SecurityModule.Aggregates.Users.Validators;
using Aritter.Infra.Crosscutting.Exceptions;
using System.Linq;

namespace Aritter.Application.Services.SecurityModule
{
    public class UserAppService : AppService, IUserAppService
    {
        private readonly IUserAccountRepository userAccountRepository;

        public UserAppService(IUserAccountRepository userAccountRepository)
        {
            Guard.IsNotNull(userAccountRepository, nameof(userAccountRepository));

            this.userAccountRepository = userAccountRepository;
        }

        public UserAccountDto AddUserAccount(AddUserAccountDto userDto)
        {
            UserAccountValidator validator = new UserAccountValidator();

            var user = userAccountRepository.Get(new HasUsername(userDto.Username) | new HasEmailSpec(userDto.Email));

            var validation = validator.ValidateUserDuplicatated(user);

            if (!validation.IsValid)
            {
                throw new ApplicationErrorException(validation.Errors.Select(p => $"{p.Message}").ToArray());
            }

            user = UserFactory.CreateAccount(userDto.Username, userDto.Password, userDto.Email);

            validation = validator.ValidateAccount(user);

            if (!validation.IsValid)
            {
                throw new ApplicationErrorException(validation.Errors.Select(p => $"{p.Message}").ToArray());
            }

            userAccountRepository.Add(user);
            userAccountRepository.UnitOfWork.CommitChanges();

            return user.ProjectedAs<UserAccountDto>();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                userAccountRepository.Dispose();
            }
        }
    }
}
