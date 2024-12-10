using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests._Templates
{
    public class SimpleCmd_Command : RequestBase<int>
    {
        private readonly int _someInputParameter;

        public SimpleCmd_Command(PlanetExplorationDbContext context,int someInputParameter)
            : base(context)
        {
            _someInputParameter = someInputParameter;
        }

        // The authorizer, validator, and handler run in that order. If any fail, the query will not be executed.

        // The authorizer is optional and can be removed if not needed
        public override IAuthorizer Authorizer => new SimpleCmd_Authorizer(DbContext);

        // The validator is optional and can be removed if not needed
        public override IValidator Validator =>
            new SimpleCmd_Validator(DbContext, _someInputParameter);

        // The handler is mandatory to have for every command
        public override IHandler<int> Handler =>
            new SimpleCmd_Handler(DbContext, _someInputParameter);
    }

    // The authorizer class is responsible for any additional authorization logic
    public class SimpleCmd_Authorizer : AuthorizerBase
    {
        public SimpleCmd_Authorizer(PlanetExplorationDbContext context)
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
    public class SimpleCmd_Validator : ValidatorBase
    {
        private readonly int _someInputParameter;

        public SimpleCmd_Validator(PlanetExplorationDbContext context, int someInputParameter)
            : base(context)
        {
            _someInputParameter = someInputParameter;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            // Obviously, this is dummy validation logic. Replace it with your own.
            await Task.CompletedTask;

            if (_someInputParameter < 0)
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "The input parameter needs to be greater than zero for some reason.");
            }

            // You can also check things in the database, if needed, such as checking if a record exists
            return await ValidResultAsync();
        }
    }

    // The handler class is responsible for executing the query
    public class SimpleCmd_Handler : HandlerBase<int>
    {
        private readonly int _someInputParameter;

        public SimpleCmd_Handler(PlanetExplorationDbContext context, int someInputParameter)
            : base(context)
        {
            _someInputParameter = someInputParameter;
        }

        public override async Task<RequestResult<int>> HandleAsync()
        {
            // Obviously, this is a dummy query. Replace it with your own.
            // Write your command logic here. This can be creating, updating, or deleting data.
            await Task.CompletedTask;

            var mathematical = _someInputParameter * _someInputParameter;

            // Return the data
            var result = new RequestResult<int>
            {
                Data = mathematical //If you don't need to return any data, you can set this to something like 0
            };

            return result;
        }
    }
}
