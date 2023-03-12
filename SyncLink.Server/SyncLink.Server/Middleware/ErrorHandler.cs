using System.Net;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Contracts.Data.Result.Exceptions;
using SyncLink.Application.Exceptions;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Middleware;

internal class ErrorHandler : IMiddleware
{
    private const string DefaultFallbackErrorMessage = "Something went wrong.";

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        return exception switch
        {
            BusinessException businessException => HandleBusinessExceptionAsync(context, businessException),
            RepositoryActionException repositoryException => HandleRepositoryActionException(context, repositoryException),
            _ => HandleUnknownError(context)
        };
    }

    private static Task HandleRepositoryActionException(HttpContext context, RepositoryActionException repositoryException)
    {
        return repositoryException.Status switch
        {
            RepositoryActionStatus.NotFound => WriteErrorResponse(context, HttpStatusCode.NotFound, repositoryException.GetClientFacingErrors()),
            RepositoryActionStatus.Conflict => WriteErrorResponse(context, HttpStatusCode.Conflict, repositoryException.GetClientFacingErrors()),
            RepositoryActionStatus.UnknownError => WriteErrorResponse(context, HttpStatusCode.InternalServerError, repositoryException.GetClientFacingErrors()),
            _ => HandleUnknownError(context)
        };
    }

    private static Task HandleBusinessExceptionAsync(HttpContext context, BusinessException businessException)
    {
        return businessException switch
        {
            AuthException authException => HandleAuthErrorAsync(context, authException),
            _ => HandleUnknownError(context)
        };
    }

    private static Task HandleAuthErrorAsync(HttpContext context, AuthException authException)
    {
        return authException switch
        {
            LoginException loginException => WriteErrorResponse(context, HttpStatusCode.Unauthorized, loginException.Errors),
            RegistrationException registrationException => WriteErrorResponse(context, HttpStatusCode.Conflict, registrationException.Errors),
            _ => HandleUnknownError(context)
        };
    }

    private static Task HandleUnknownError(HttpContext context)
    {
        return WriteErrorResponse(context, HttpStatusCode.InternalServerError, new[] { DefaultFallbackErrorMessage });
    }

    private static Task WriteErrorResponse(HttpContext context, HttpStatusCode statusCode, IEnumerable<string>? errors)
    {
        return context.Response.WriteAsJsonAsync(new ErrorDetails
        {
            StatusCode = statusCode,
            Errors = errors?.ToList()
        });
    }
}