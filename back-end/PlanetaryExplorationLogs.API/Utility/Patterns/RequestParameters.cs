namespace PlanetaryExplorationLogs.API.Utility.Patterns
{
    public class QueryParameters<T>
    {
        public List<Func<T, bool>> Filters { get; set; }
        public List<(Func<T, object> OrderBy, bool Descending)> SortOrders { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public QueryParameters()
        {
            Filters = new List<Func<T, bool>>();
            SortOrders = new List<(Func<T, object>, bool)>();
        }
    }

    public class BasicQueryFilters
    {
        public string TextSearchFilter { get; set; } = "";
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 25;
    }
}
