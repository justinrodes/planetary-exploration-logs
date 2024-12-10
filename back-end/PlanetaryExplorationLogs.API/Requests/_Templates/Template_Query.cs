using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests._Templates
{
    public class Record_Query : RequestBase<string>
    {
        private readonly string _someFilter;

        public Record_Query(PlanetExplorationDbContext context, string someFilter)
            : base(context)
        {
            _someFilter = someFilter;
        }

        // The authorizer, validator, and handler run in that order. If any fail, the query will not be executed.

        // The authorizer is optional and can be removed if not needed
        public override IAuthorizer Authorizer => new Record_Authorizer(DbContext);

        // The validator is optional and can be removed if not needed
        public override IValidator Validator => new Record_Validator(DbContext, _someFilter);

        // The handler is mandatory to have for every query
        public override IHandler<string> Handler => new Record_Handler(DbContext, _someFilter);
    }

    // The handler class is responsible for executing the query
    public class Record_Handler : HandlerBase<string>
    {
        private readonly string _someFilter;

        public Record_Handler(PlanetExplorationDbContext context, string someFilter)
            : base(context)
        {
            _someFilter = someFilter;
        }

        public override async Task<RequestResult<string>> HandleAsync()
        {
            // Obviously, this is a dummy query. Replace it with your own.
            // Write your query here
            var somePlanet = await DbContext.Planets.Where(u => u.Name.Contains(_someFilter)).FirstAsync();

            // Return the data
            var result = new RequestResult<string> { Data = somePlanet.Name };

            return result;
        }
    }

    // The authorizer class is responsible for any additional authorization logic
    public class Record_Authorizer : AuthorizerBase
    {
        public Record_Authorizer(PlanetExplorationDbContext context)
            : base(context)
        {
        }

        public override async Task<RequestResult> AuthorizeAsync()
        {
            // Obviously, this is dummy authorization logic. Replace it with your own.
            // Return AuthorizedResultAsync() if the operation is authorized, UnauthorizedResultAsync() otherwise.
            return await AuthorizedResultAsync();
        }
    }

    // The validator class is responsible for validating things before the query is executed
    public class Record_Validator : ValidatorBase
    {
        private readonly string _someFilter;

        public Record_Validator(PlanetExplorationDbContext context, string someFilter)
            : base(context)
        {
            _someFilter = someFilter;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            // Obviously, this is dummy validation logic. Replace it with your own.
            await Task.CompletedTask;
            if (string.IsNullOrEmpty(_someFilter))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "Need to have a value for the filter, apparently.");
            }

            // You can also check things in the database, if needed, such as checking if a record exists
            return await ValidResultAsync();
        }
    }
}
