


// HttpClient client = new();
// Task<HttpResponseMessage> task = client.GetAsync("http://nfl.com");

// task.ContinueWith(task => {
//     HttpResponseMessage response = task.Result;
//     Console.WriteLine(response.StatusCode);
// });

async void PrintStatusCodeAsync () 
{
    HttpClient client = new();
    HttpResponseMessage response = await client.GetAsync("http://nfl.com");
    Console.WriteLine(response.StatusCode);
}

Console.WriteLine("Program start.");

Console.WriteLine("Starting background process...");

PrintStatusCodeAsync();

Console.WriteLine("Proceeding with program...");

Console.WriteLine("Program end.");
Console.ReadLine();