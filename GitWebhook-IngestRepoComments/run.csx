using System.Net;

/*
    When a new issue is submitted on my github repo, it sends a json payload to this function.
    outgitissueseventhub is assigned the payload, which sends it off to an event hub
*/
public static HttpResponseMessage Run(HttpRequestMessage req, TraceWriter log, out string outgitissueseventhub)
{
    // Get request body
    string data = req.Content.ReadAsStringAsync().Result;
    
    // log to console
    log.Info(data);

    // This sends the event data (json) to the eventhub
    outgitissueseventhub = data;
    
    // Always responding with 200 OK for simple demo
    return req.CreateResponse(HttpStatusCode.OK);
}