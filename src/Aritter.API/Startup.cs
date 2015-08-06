﻿using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Aritter.API.Startup))]

namespace Aritter.API
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);

			HttpConfiguration config = new HttpConfiguration();

			WebApiConfig.Register(config);
			app.UseCors(CorsOptions.AllowAll);
			app.UseWebApi(config);
		}
	}
}
