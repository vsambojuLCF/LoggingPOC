using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using LoggingDemo;
public class Program
{
    public static int count_Method1 = 0;
    public static int count_Method2 = 0;
    public static void Main(string[] args)
    {
        Program p = new Program();
        LogData logData = new LogData();
        Log.Logger = new LoggerConfiguration().WriteTo.File("Logs/ApplicationTracker.txt")
            .CreateLogger();
        using var factory = LoggerFactory.Create(builder =>
        {
            builder.AddSerilog();
        });
        ILogger<Program> logger = factory.CreateLogger<Program>();
        logger.LogInformation("Hello World! Logging is {Description}", "fun");
        Console.WriteLine("Hello Teja");
        string input = "";
        var stopWatch = Stopwatch.StartNew();
        while (input!="0") 
        {
            Console.WriteLine("Enter your input (Either 1 - Method1 or 2 - Method2 or 0 - ExitApplication: )");
             input = Console.ReadLine();
            switch(input)
            {
                case "1":
                    p.TestMethod1();
                    break;
                case "2":
                    p.TestMethod2(); 
                    break;
                case "0":
                    Console.WriteLine("Exiting application");
                    break;
                default:
                    Console.WriteLine("Enter valid input");
                    break;

            }
        }
        stopWatch.Stop();
        logger.LogInformation("The application is active for {Elapsed_Time} before exiting", stopWatch.Elapsed.TotalSeconds);
        logger.LogInformation("Number of times Method1 was hit is {Method1_hits}", count_Method1);
        logger.LogInformation("Number of times Method2 was hit is {Method2_hits}", count_Method2);
        Console.WriteLine(logger.ToString);
        Log.CloseAndFlush();
    }

    public void TestMethod1()
    {
        count_Method1++;
    }

    public void TestMethod2() 
    {
        count_Method2++;
    }

}