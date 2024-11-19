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

// How NOT to do it (blocking)
string s = someTask.Result;
```

---
End of document