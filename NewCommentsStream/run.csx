using System;
/*
    This function triggers on new events in my event hub. When a new event is added,
    the json payload of it is output to the screen.

 */
public static void Run(string myEventHubMessage, TraceWriter log)
{
    log.Info($"C# Event Hub trigger function processed a message: {myEventHubMessage}");
}