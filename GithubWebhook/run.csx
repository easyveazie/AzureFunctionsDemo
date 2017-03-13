#load "..\shared\vzLogger.csx"

using System.Net; 

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Use the shared logger to write this to our eventhub stream
    WriteCustomLogEvent(data.ToString());
    return req.CreateResponse(HttpStatusCode.OK);
}