using System.Net;

public static HttpResponseMessage Run(HttpRequestMessage req, TraceWriter log, out string outgitissueseventhub)
{
    // Get request body
    string data = req.Content.ReadAsStringAsync().Result;
    
    // log to console
    log.Info(data);

    // This sends the event data (json) to the even thub
    outgitissueseventhub = data;
    
    return req.CreateResponse(HttpStatusCode.OK, "From Github:");
}