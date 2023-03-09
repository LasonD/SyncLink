using System.Net;

namespace SyncLink.Server.Dtos;

public class ErrorDetails
{
    public HttpStatusCode StatusCode { get; set; }

    public IEnumerable<string>? Errors { get; set; }
}