using System.Net;

namespace PlanetaryExplorationLogs.API.Utility.Patterns
{
    public class RequestResult<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public T? Data { get; set; }
    }

    public class RequestResult
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
