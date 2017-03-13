#r "NewtonSoft.Json"

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

public enum LogLevel {Low, Medium, High, Error };

public class CustomLogEvent
{
    public string Message { get; set;}   
    
    public string Level { get; set; }

    public string CallerMemberName { get; set; }    

    public int CallerLineNumber { get; set; }

    public string CallerFilePath { get; set; }
    
    public string SerializeAsJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public static string GetSecret(string name)
{
    return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
}

public static void WriteCustomLogEvent(
        string message = "",
        LogLevel level = LogLevel.Medium,
        [CallerMemberName] string callerName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0
    )
{
	var eventObj = new CustomLogEvent(){
	    Message = message,
	    Level = Enum.GetName(typeof(LogLevel),level),
	    CallerMemberName = callerName,        
	    CallerLineNumber = callerLineNumber,
	    CallerFilePath = callerFilePath
	};    
    
    var eventInfoAsJson = eventObj.SerializeAsJson();
	
    var logFunctionUri = "https://vzfunctions.azurewebsites.net/api/HttpLogger";
    var logFunctionParams = "?code=" + GetSecret("HTTPLOGGER_FUNCTIONKEY");

    HttpClient client = new HttpClient();
    client.BaseAddress = new Uri(logFunctionUri);

    // Add an Accept header for JSON format.
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));	
									
    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, logFunctionParams);
    request.Content = new StringContent(eventInfoAsJson,
                                    Encoding.UTF8, 
                                    "application/json");

    client.SendAsync(request);    
}