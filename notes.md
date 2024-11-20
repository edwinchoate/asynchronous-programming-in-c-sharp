# Notes

## Ch. 1 Understanding Asynchronous Programming

_asynchronous programming_ - the technique that lets you offload long-running operations to background threads so that the main thread of your application remains available to respond to new input from your user.

* Useful for performing tasks that take long to execute: reading a file, making an HTTP request, etc.
* Prevents the UI from blocking the main thread
    * Can be perceived as the app freezing
    * Can slow the user down unecessarily 

> _Asynchronous code can send those long-running tasks to separate threads._

* Application Process
    * Read a file (Thread)
    * User Interface (Thread)
    * Make HTTP request (Thread)

When to consider an asynchronous solution: 

1. **I/O** - reading or writing from disk or stream
    * Examples
        * Read/write data to/from a file
        * Making an API call to a webservice
        * Querying a database
2. **Computation** - intensive CPU processing
    * Examples
        * Iterating over very large datasets 
        * Performing complex math calculations

## Ch. 2 Working with Tasks

[Task Class](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task?view=net-8.0)

How to use `Task` to call a method asynchronously on another thread: 

```C#
using System.Threading.Tasks;

Task.Run(SomeMethodName);
// Or,
var myTask = new Task(SomeMethodName);
myTask.Start();
```

* Using `.Wait()` or `.Result` can be _blocking_ (ex: if you get .Result without using a continuation).

_Continuations_ - tasks invoked by other tasks (like callbacks) that start when a previous task completes.

* Continuations are customizable with `TaskContinuationOptions` enum. You can configure them to begin after one specific task completes, any one of a group completes, after all in a group complete, etc.

How to properly use a continuation to get the results of an asychronous task: 

```C#
Task<string> someTask = Task.Run(SomeMethodName); // Use Task<T> to anticipate the return type

someTask.ContinueWith(task => {
    string s = task.Result; // Result is type T
});

// Making a direct call to .Result is practically synchronous - it waits for the task to finish (and is blocking)
string s = someTask.Result;
```

## Ch. 3 Using `async` and `await`

`async` - tells the compiler that a function does some asynchronous work

`await` - tells the compiler what code performs the asychronous task. The main thread is allowed to continue to run while awaiting. Once the await code is finished, the rest of the async function continues to execute 

```C#
async Task ProcessDataAsync () 
{
    string data = await ReadFromDatabase();
    ...
}
```

* An `async` function should return `Task`, `Task<T>`, or `void`.
* It's conventional to use the suffix `Async` on asynchronous functions.

### Handling Exceptions with Asynchronous Tasks

* Exceptions that occur in async tasks are stored on the `Task.Exception` property of the Task. 
* The `Task.Exception` property is of type `AggregateException`
    * `AggregateExeception` is a collection of potentially more than one exception 
* Exceptions thrown from a `Wait()` or `Result` call will be bundled together as an `AggregateException` and you'll have to iterate through the exceptions to see the specifics. 
* On the other hand, `await` will throw the first exception of the `AggregateException`

_Tip:_ https://expired.badssl.com/ lets you test expired SSL certificates

This prints `System.AggregateException`, because it uses `.Result`:

```C#
async void ProcessDataAsync () 
{
    try 
    {
        Task<string> task = ReadFromDatabase();
        string data = task.Result;
        // Assuming an exception occurs
    }
    catch (Exception e) 
    {
        Console.WriteLine(e.GetType()); // -> AggregateException
    }
}
```

* To see the specific exception, you'd look in the `InnerException` property

However, this prints the specific exception: 

```C#
async void ProcessDataAsync () 
{
    try 
    {
        string data = await ReadFromDatabase();
        // Assuming an exception occurs
    }
    catch (Exception e) 
    {
        Console.WriteLine(e.GetType()); // -> FileNotFoundException (or whatever the first exception was)
    }
}
```

---
End of document