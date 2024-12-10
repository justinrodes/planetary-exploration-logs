using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests._Templates
{
    public class MultipleRecordsPaged_Query : RequestBase<PagedDataResult<string>>
    {
        private readonly BasicQueryFilters _filters;

        public MultipleRecordsPaged_Query(
            PlanetExplorationDbContext context,
            BasicQueryFilters filters
        )
            : base(context)
        {
            _filters = filters;
        }

        // The authorizer, validator, and handler run in that order. If any fail, the query will not be executed.

        // The authorizer is optional and can be removed if not needed
        public override IAuthorizer Authorizer =>
            new MultipleRecordsPaged_Authorizer(DbContext);

        // The validator is optional and can be removed if not needed
        public override IValidator Validator =>
            new MultipleRecordsPaged_Validator(DbContext, _filters);

        // The handler is mandatory to have for every query
        public override IHandler<PagedDataResult<string>> Handler =>
            new MultipleRecordsPaged_Handler(DbContext, _filters);
    }

    // The handler class is responsible for executing the query
    public class MultipleRecordsPaged_Handler : HandlerBase<PagedDataResult<string>>
    {
        private readonly BasicQueryFilters _filters;

        public MultipleRecordsPaged_Handler(PlanetExplorationDbContext context, BasicQueryFilters filters)
            : base(context)
        {
            _filters = filters;
        }

        public override async Task<RequestResult<PagedDataResult<string>>> HandleAsync()
        {
            // Write your query here and make sure it is .AsQueryable()
            var fruit = new List<string>
            {
                "Apple",
                "Banana",
                "Cherry",
                "Date",
                "Elderberry",
                "Fig",
                "Grape",
                "Honeydew"
            };
            var fruitQuery = fruit.AsQueryable();

            // Pass in your query, page size, and page number
            var pagedData = await GetPagedDataAsync(
                fruitQuery,
                _filters.PageSize,
                _filters.PageNumber
            );

            // Return the paged data
            var result = new RequestResult<PagedDataResult<string>> { Data = pagedData };

            return result;
        }
    }

    // The authorizer class is responsible for any additional authorization logic
    public class MultipleRecordsPaged_Authorizer : AuthorizerBase
    {
        public MultipleRecordsPaged_Authorizer(PlanetExplorationDbContext context)
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
    public class MultipleRecordsPaged_Validator : ValidatorBase
    {
        private readonly BasicQueryFilters _filters;

        public MultipleRecordsPaged_Validator(PlanetExplorationDbContext context, BasicQueryFilters filters)
            : base(context)
        {
            _filters = filters;
        }

        public async override Task<RequestResult> ValidateAsync()
        {
            // Obviously, this is dummy validation logic. Replace it with your own.
            await Task.CompletedTask;

            if (_filters.PageNumber < 1)
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "Current page must be greater than zero.");
            }

            if (_filters.PageSize < 1)
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "Number of items per page must be greater than zero.");
            }

            // You can also check things in the database, if needed, such as checking if a record exists
            return await ValidResultAsync();
        }
    }
}
