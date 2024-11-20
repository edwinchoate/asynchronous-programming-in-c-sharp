
List<int> CalculateFactors (int n) 
{
    List<int> factors  = new();

    for (int i = 1; i <= n; i++) 
    {
        if (n % i == 0) 
            factors.Add(i);
    }
    
    return factors;
}

Console.WriteLine("Program start.");

Console.WriteLine("Starting background thread...");

Task<List<int>> calcFactorsTask = Task.Run(() => CalculateFactors(100));

Console.WriteLine("Background thread started.");

calcFactorsTask.ContinueWith(task => {
    foreach (int i in task.Result) 
    {
        Console.Write(i + " ");
    }
    Console.WriteLine();
    Console.WriteLine("Background thread complete.");
});

Console.WriteLine("Continuing program...");
Console.ReadLine();

Console.WriteLine("Program end.");