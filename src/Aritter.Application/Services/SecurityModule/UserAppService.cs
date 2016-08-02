﻿using Aritter.Application.DTO.SecurityModule.Authentication;
using Aritter.Application.DTO.SecurityModule.Users;
using Aritter.Application.Seedwork.Extensions;
using Aritter.Application.Seedwork.Services;
using Aritter.Application.Seedwork.Services.SecurityModule;
using Aritter.Domain.SecurityModule.Aggregates.MainAgg;
using Aritter.Domain.SecurityModule.Aggregates.UserAgg;
using Aritter.Domain.SecurityModule.Aggregates.UserAgg.Validators;
using Aritter.Infra.Crosscutting.Exceptions;
using System.Linq;

namespace Aritter.Application.Services.SecurityModule
{
	public class UserAppService : AppService, IUserAppService
	{
		private readonly IUserRepository userRepository;

		public UserAppService(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public UserDto CreateUser(AddUserDto addUserDto)
		{
			var person = PersonFactory.CreatePerson(addUserDto.FirstName, addUserDto.LastName);
			var user = UserFactory.CreateUser(person, addUserDto.Username, addUserDto.Password, addUserDto.Email);

			UserValidator validator = new UserValidator();
			var validation = validator.ValidateUser(user);

			if (!validation.IsValid)
			{
				throw new ApplicationErrorException(validation.Errors.Select(p => $"{p.Message}"));
			}

			userRepository.Add(user);
			userRepository.UnitOfWork.CommitChanges();

			return user.ProjectedAs<UserDto>();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				userRepository.Dispose();
			}
		}
	}
}
