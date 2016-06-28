﻿using Aritter.Infra.Web.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace Aritter.Infra.Web.Filters
{
	public class RequestValidatorAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (!actionContext.ModelState.IsValid)
			{
				actionContext.Response = CreateErrorResponse(actionContext);
				return;
			}

			base.OnActionExecuting(actionContext);
		}

		private HttpResponseMessage CreateErrorResponse(HttpActionContext actionContext)
		{
			var modelErrors = GetModelErrors(actionContext.ModelState.Values);
			var response = GetErrorResponse(modelErrors);

			return actionContext.Request.CreateResponse(HttpStatusCode.OK, response);
		}

		private ErrorResponse GetErrorResponse(IEnumerable<string> modelErrors)
		{
			return new ErrorResponse(modelErrors.ToArray());
		}

		private IEnumerable<string> GetModelErrors(ICollection<ModelState> modelStateValues)
		{
			return modelStateValues
				.SelectMany(p => p.Errors)
				.Select(p => p.ErrorMessage);
		}
	}
}