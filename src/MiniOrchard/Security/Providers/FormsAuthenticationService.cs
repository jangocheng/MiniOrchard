﻿using System;
using System.Web;
using System.Web.Security;
using MiniOrchard.Data;
using MiniOrchard.Logging;
using MiniOrchard.Mvc;
using MiniOrchard.Services;

namespace MiniOrchard.Security.Providers
{
	public class FormsAuthenticationService : IAuthenticationService
	{
		private readonly IClock _clock;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private IUser _signedInUser;
		private IRepository<IUser> _repo;

		public FormsAuthenticationService(IClock clock, IHttpContextAccessor httpContextAccessor)//, IRepository<IUser> repo)
		{
			_clock = clock;
			_httpContextAccessor = httpContextAccessor;
			//_repo = repo;

			Logger = NullLogger.Instance;

			ExpirationTimeSpan = TimeSpan.FromDays(30);
		}

		public ILogger Logger { get; set; }

		public TimeSpan ExpirationTimeSpan { get; set; }

		public void SignIn(IUser user, bool createPersistentCookie)
		{
			var now = _clock.UtcNow.ToLocalTime();
			var userData = Convert.ToString(user.Id);

			var ticket = new FormsAuthenticationTicket(
				1 /*version*/,
				user.UserName,
				now,
				now.Add(ExpirationTimeSpan),
				createPersistentCookie,
				userData,
				FormsAuthentication.FormsCookiePath);

			var encryptedTicket = FormsAuthentication.Encrypt(ticket);

			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
			cookie.HttpOnly = true;
			cookie.Secure = FormsAuthentication.RequireSSL;
			cookie.Path = FormsAuthentication.FormsCookiePath;
			if (FormsAuthentication.CookieDomain != null)
			{
				cookie.Domain = FormsAuthentication.CookieDomain;
			}

			if (createPersistentCookie)
			{
				cookie.Expires = ticket.Expiration;
			}

			var httpContext = _httpContextAccessor.Current();
			httpContext.Response.Cookies.Add(cookie);
			_signedInUser = user;
		}

		public void SignOut()
		{
			_signedInUser = null;
			FormsAuthentication.SignOut();
		}

		public void SetAuthenticatedUserForRequest(IUser user)
		{
			_signedInUser = user;
		}

		public IUser GetAuthenticatedUser()
		{
			if (_signedInUser != null)
				return _signedInUser;

			var httpContext = _httpContextAccessor.Current();
			if (httpContext == null || !httpContext.Request.IsAuthenticated || !(httpContext.User.Identity is FormsIdentity))
			{
				return null;
			}

			var formsIdentity = (FormsIdentity)httpContext.User.Identity;
			var userData = formsIdentity.Ticket.UserData;
			int userId;
			if (!int.TryParse(userData, out userId))
			{
				Logger.Fatal("User id not a parsable integer");
				return null;
			}
			return null;
			//return _repo.Get(userId);
		}
	}
}
