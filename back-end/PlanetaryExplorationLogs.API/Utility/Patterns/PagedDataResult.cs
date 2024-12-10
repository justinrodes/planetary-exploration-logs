namespace PlanetaryExplorationLogs.API.Utility.Patterns
{
    public class PagedDataResult<T>
    {
        public List<T> PageData { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public PagedDataResult()
        {
            PageData = new List<T>();
        }
    }
}
