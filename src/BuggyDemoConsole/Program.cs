﻿using BuggyDemoConsole.Models;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;

Console.WriteLine("Press the");
Console.WriteLine("1) Crash - Null reference exceptions.");
Console.WriteLine("2) Crash - GC Heap presssure, OOM Exceptions.");
Console.WriteLine("3) Crash - Unhandled Exception. Call Stack.");
Console.WriteLine("4) Crash - Another Null reference exceptions.");

ConsoleKeyInfo keyReaded = Console.ReadKey();
Console.WriteLine();

switch (keyReaded.Key)
{
    case ConsoleKey.D1: 
        NullReferenceException();
        break;

    case ConsoleKey.D2:
        OutOfMemoryException();
        break;

    case ConsoleKey.D3:
        await A();
        break;

    case ConsoleKey.D4:
        await NullReferenceException2();
        break;

    case ConsoleKey.D5:
        CopilotExceptionExample1();
        break;

    case ConsoleKey.D6:
        CopilotExceptionExample2();
        break;

    default: //Not known key pressed
        Console.WriteLine("Wrong key, please try again.");
        break;
}

Console.WriteLine("Hit any key to exit");
Console.ReadKey();

static void NullReferenceException()
{
    var fu = new Foo();
    
    var name = fu.Bar.Baz.Name;
}

static async Task NullReferenceException2()
{

    HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://www.poppastring.com"),
    };
  
    using HttpResponseMessage response = await sharedClient.GetAsync(".well-known/webfinger?resource=acct:poppastring@dotnet.social");

    var jsonresponse = await response.Content.ReadAsStringAsync();

    User? userinfo = JsonSerializer.Deserialize<User>(jsonresponse);

    Console.WriteLine(userinfo.person.firstname);
}


static void OutOfMemoryException()
{
    List<Product> products = new List<Product>();
    string answer = "";
    do
    {
        for (int i = 0; i < 10000; i++)
        {
            products.Add(new Product(i, "product" + i));
        }
        Console.WriteLine("Leak some more? Y/N");
        answer = Console.ReadLine().ToUpper();

    } while (answer == "Y");
}

static async Task A()
{
    await B();
}

static async Task B()
{
    await C();
}

static async Task C()
{
    await Task.Delay(3000);
    throw new Exception();
}

static void CopilotExceptionExample1()
{
    var resp = new MyDataResponse() { Message = "Some message...", Status = IntPtr.Parse("1") };

    // 1. We return a json value of the data
    var str = JsonSerializer.Serialize(resp);

    // 2. Copilot: Explain the NotSupportedException
    // 3. Copilot: Give me an JsonExport example that supports IntPtr.Zero
    // 4. Copilot: Show me how to call the JsonExportExample

    Console.WriteLine(str);
}

static void CopilotExceptionExample2()
{
    var people = new List<string> { "Alfred Archer", "Billy Baller", "Billy Bob", "Cathy Carter" };

    // var zane = people.First(x => x.StartsWith("Zane"));
    var billy = people.SingleOrDefault(x => x.StartsWith("Billy"));
    var billy2 = people.Single(x => x.StartsWith("Billy"));
}

public class MyDataResponse
{
    public string Message { get; set; }
    public IntPtr Status { get; set; }
}




