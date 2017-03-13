#load "..\shared\vzLogger.csx"
#r "NewtonSoft.Json"

using System.Net;
using Newtonsoft.Json;

public class Person
{
    public string First { get; set; }

    public string Last { get; set; }
}

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    try
    {
        dynamic data = await req.Content.ReadAsAsync<object>();

        // Grab the request body and write the output to our event hub
        var reqBodyContent = JsonConvert.DeserializeObject<Person>(data.ToString());
        var msg = "First: " + reqBodyContent.First + "Last: " + reqBodyContent.Last;
        WriteCustomLogEvent(msg);

        return req.CreateResponse(HttpStatusCode.OK);
    }
    catch
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
    }
}