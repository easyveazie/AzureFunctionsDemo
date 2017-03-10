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
        // Get request body
        dynamic data = await req.Content.ReadAsAsync<object>();

        var reqBodyContent = JsonConvert.DeserializeObject<Person>(data.ToString());
        log.Info("First: " + reqBodyContent.First);
        log.Info("Last: " + reqBodyContent.Last);

        
        return req.CreateResponse(HttpStatusCode.OK);
    }
    catch
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
    }
}