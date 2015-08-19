﻿using Aritter.Domain.Aggregates;
using Aritter.Domain.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aritter.Application.Managers
{
	public class UserManager : ApplicationManager, IUserManager
	{
		private readonly IUserDomainService userDomainService;

		public UserManager(IUserDomainService userDomainService)
		{
			if (userDomainService == null)
				throw new ArgumentNullException(nameof(userDomainService));

			this.userDomainService = userDomainService;
		}

		public bool CheckChangePasswordRequired(int userId)
		{
			return userDomainService.CheckChangePasswordRequired(userId);
		}

		public User GetUser(int id)
		{
			return userDomainService.GetUser(id);
		}

		public IEnumerable<Resource> GetMenus(int userId)
		{
			return userDomainService
				.GetMenus(userId);
		}

		public IEnumerable<Rule> GetRules(int userId, string area, string controller, string action)
		{
			return userDomainService
				.GetRules(userId, area, controller, action);
		}

		public int AuthenticateUser(string username, string password)
		{
			return userDomainService
				.AuthenticateUser(username, password);
		}

		public ResetPasswordResult ResetPassword(string mailAddress)
		{
			return userDomainService.ResetPassword(mailAddress);
		}

		public User GetUserBySecurityToken(string token)
		{
			return userDomainService.GetUserBySecurityToken(token);
		}

		public void ChangePassword(int userId, string currentPassword, string newPassword)
		{
			userDomainService.ChangePassword(userId, currentPassword, newPassword);
		}

		public async Task<IUser> FindAsync(string userName, string password)
		{
			return await Task.FromException<IUser>(new NotImplementedException());
		}

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(string authenticationType)
		{
			return await Task.FromException<ClaimsIdentity>(new NotImplementedException());
		}
	}
}