﻿using Aritter.Infra.Crosscutting.Exceptions;
using Aritter.Infra.Crosscutting.Logging;
using Aritter.Infra.Web.Messages;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Aritter.Infra.Web.Filters
{
    public sealed class AritterExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public AritterExceptionFilterAttribute()
            : base()
        {
            logger = LoggerFactory.CreateLog();
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            LogException(context.Exception);
            context.Response = CreateErrorResponse(context);
        }

        private HttpResponseMessage CreateErrorResponse(HttpActionExecutedContext context)
        {
            var response = new ErrorResponse();

            if (context.Exception is ApplicationErrorException)
            {
                response.Reject((context.Exception as ApplicationErrorException).ApplicationErrors.ToArray());
            }
            else
            {
                response.Reject("There was an unexpected error and the operation was canceled.");
            }

            return context.Request.CreateResponse(HttpStatusCode.OK, response);
        }

        private void LogException(Exception ex)
        {
            logger.Error($"===== Begin Service Exception =====");
            logger.Error($"TransactionAbortedException Message: {ex.Message}", ex);

            Exception current = ex;

            while (current != null)
            {
                logger.Error($"TransactionAbortedException Message: {current.Message}", current);
                current = current.InnerException;
            }

            logger.Error($"===== End Service Exception =====");
        }
    }
}