
// This represents a blocking synchronous function
void ProcessData () 
{
    Console.WriteLine("Processing data...");
    Thread.Sleep(3000);
    Console.WriteLine("Processing complete.");
}


Console.WriteLine("Program starting...");

// ProcessData(); // BLOCKING
Task.Run(ProcessData); // NON-BLOCKING

HttpClient client = new();
Task<HttpResponseMessage> responseTask = client.GetAsync("http://nfl.com");

// HttpResponseMessage response = responseTask.Result; // BLOCKING

// NON-BLOCKING
responseTask.ContinueWith(task => {
    HttpResponseMessage response = task.Result;
});

Console.WriteLine("Ready for input.");
Console.ReadLine();