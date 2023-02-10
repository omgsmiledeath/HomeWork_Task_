// See https://aka.ms/new-console-template for more information

using Test;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Start Main");
        var cwr = new ConsoleWriter(20);
        for (int i = 0; i <= 20; i++)
        {
            cwr.SetMessage(Guid.NewGuid().ToString());
            cwr.setColor((ConsoleColor)(new Random(DateTime.Now.Microsecond).Next(1,13)));

        }

        Console.WriteLine("End fiil messageBook");
        Console.WriteLine("Start ConsoleWriter.Start()");
        
         cwr.Start();
        
        Console.WriteLine("END MAIN");
        
    }
}