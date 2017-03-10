#load "..\shared\vzlogger.csx"

public static void Run(HttpRequestMessage req, out string outputEventHubMessage, TraceWriter log)
{    
    var data = req.Content.ReadAsAsync<object>().Result;
    WriteCustomLogEvent(data.ToString());
    outputEventHubMessage = data.ToString();
}