using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using System.Net;

namespace PlanetaryExplorationLogs.API.Utility.Patterns
{
    public class CommandQuery
    {
        public interface IAuthorizer
        {
            Task<RequestResult> AuthorizeAsync();
            string ErrorMessage { get; set; }
            HttpStatusCode ErrorCode { get; set; }
        }

        public interface IValidator
        {
            Task<RequestResult> ValidateAsync();
            string ErrorMessage { get; set; }
            HttpStatusCode ErrorCode { get; set; }
        }

        public interface IHandler<T>
        {
            Task<RequestResult<T>> HandleAsync();
        }

        public interface IRequest<T>
        {
            IAuthorizer? Authorizer { get; }
            IValidator? Validator { get; }
            IHandler<T> Handler { get; }

            Task<RequestResult<T>> ExecuteAsync();
        }

        public abstract class RequestBase<T> : IRequest<T>
        {
            protected PlanetExplorationDbContext DbContext { get; }

            public virtual IAuthorizer? Authorizer => null;
            public virtual IValidator? Validator => null;
            public abstract IHandler<T> Handler { get; }

            protected RequestBase(PlanetExplorationDbContext context)
            {
                DbContext = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<RequestResult<T>> ExecuteAsync()
            {
                using var transaction = DbContext.Database.BeginTransaction();
                try
                {
                    if (Authorizer != null)
                    {
                        var authorizationResult = await Authorizer.AuthorizeAsync();
                        if (!authorizationResult.Success)
                        {
                            return new RequestResult<T>
                            {
                                Success = false,
                                Message = authorizationResult.Message ?? "Authorization failed",
                                StatusCode =
                                    authorizationResult.StatusCode != default
                                        ? authorizationResult.StatusCode
                                        : HttpStatusCode.Unauthorized
                            };
                        }
                    }

                    if (Validator != null)
                    {
                        var validationResult = await Validator.ValidateAsync();
                        if (!validationResult.Success)
                        {
                            return new RequestResult<T>
                            {
                                Success = false,
                                Message = validationResult.Message ?? "Validation failed",
                                StatusCode =
                                    validationResult.StatusCode != default
                                        ? validationResult.StatusCode
                                        : HttpStatusCode.BadRequest
                            };
                        }
                    }

                    var result = await Handler.HandleAsync();
                    transaction.Commit();
                    return result;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    string errorMessage = "Outer Exception: " + e.Message;
                    if (e.InnerException != null)
                    {
                        errorMessage += " Inner Exception: " + e.InnerException.Message;
                    }

                    return new RequestResult<T>
                    {
                        Success = false,
                        Message = e.ToString(),
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }
            }
        }

        public abstract class AuthorizerBase : IAuthorizer
        {
            protected PlanetExplorationDbContext DbContext { get; }
            public string ErrorMessage { get; set; } = "";
            public HttpStatusCode ErrorCode { get; set; }

            public abstract Task<RequestResult> AuthorizeAsync();

            protected AuthorizerBase(PlanetExplorationDbContext context)
            {
                DbContext = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<RequestResult> AuthorizedResultAsync()
            {
                await Task.CompletedTask;
                return new RequestResult();
            }

            public async Task<RequestResult> UnauthorizedResultAsync(HttpStatusCode statusCode, string message)
            {
                await Task.CompletedTask;
                return new RequestResult
                {
                    Success = false,
                    Message = message,
                    StatusCode = statusCode
                };
            }
        }

        public abstract class ValidatorBase : IValidator
        {
            protected PlanetExplorationDbContext DbContext { get; }
            public string ErrorMessage { get; set; } = "";
            public HttpStatusCode ErrorCode { get; set; }

            public abstract Task<RequestResult> ValidateAsync();

            protected ValidatorBase(PlanetExplorationDbContext context)
            {
                DbContext = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<RequestResult> ValidResultAsync()
            {
                await Task.CompletedTask;
                return new RequestResult();
            }

            public async Task<RequestResult> InvalidResultAsync(HttpStatusCode statusCode, string message)
            {
                await Task.CompletedTask;
                return new RequestResult
                {
                    Success = false,
                    Message = message,
                    StatusCode = statusCode
                };
            }
        }

        public abstract class HandlerBase<T> : IHandler<T>
        {
            protected PlanetExplorationDbContext DbContext { get; }

            public abstract Task<RequestResult<T>> HandleAsync();

            protected HandlerBase(PlanetExplorationDbContext context)
            {
                DbContext = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<PagedDataResult<R>> GetPagedDataAsync<R>(
                IQueryable<R> filteredData,
                int pageSize,
                int pageNumber
            )
            {
                var query = filteredData;

                var count = await query.CountAsync();

                List<R> pageData;
                if (pageSize > 0)
                {
                    pageData = await query
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }
                else
                {
                    pageData = await query.ToListAsync();
                }

                return new PagedDataResult<R>
                {
                    PageData = pageData,
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalPages = pageSize > 0 ? (int)Math.Ceiling(count / (double)pageSize) : 1,
                    TotalCount = count
                };
            }
        }
    }
}
