

async Task GetPage (string Uri) 
{
    HttpClient client = new();
    CancellationTokenSource tokenSource = new();
    tokenSource.CancelAfter(1);

    try 
    {
        string body = await client.GetStringAsync(Uri, tokenSource.Token);
        Console.WriteLine(body);
    }
    catch (TaskCanceledException) 
    {
        Console.WriteLine("Error: Request timed out.");
    }
    finally 
    {
        tokenSource.Dispose();
    }
}


Console.WriteLine("Starting program.");

Console.WriteLine("Calling get page...");

Task task = GetPage("http://example.com");

Console.WriteLine("Called get page.");

Console.WriteLine("Program end. Press any key to exit.");
Console.ReadKey();