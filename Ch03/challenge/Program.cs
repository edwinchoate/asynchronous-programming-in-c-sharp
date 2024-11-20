
async void PrintUrl (string url) 
{
    try 
    {
        Thread.Sleep(5000);
        HttpClient client = new();
        string contents = await client.GetStringAsync(url);
        Console.WriteLine(contents);
    }
    catch (Exception e) 
    {
        Console.WriteLine(e.Message);
    }
}

Console.WriteLine("Program start.");

Console.WriteLine("Starting background process...");

PrintUrl("https://example.com");

Console.WriteLine("Continuing program execution.");
Console.WriteLine();

Console.ReadLine();
Console.WriteLine("Program end.");