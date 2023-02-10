// See https://aka.ms/new-console-template for more information

using Test;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine($"Начало Main {Thread.CurrentThread.ManagedThreadId}");
        var cwr = new ConsoleWriter(200);

        for (int i = 0; i <= 200; i++)
        {
            cwr.SetMessage(Guid.NewGuid().ToString());
            var color = ((ConsoleColor)(new Random(DateTime.Now.Microsecond).Next(1,13)));
            cwr.setColor(color);

        }

        Console.WriteLine("Сгенерировали все данные для вывода");
        Console.WriteLine("Запускаем ConsoleWriter.Start()");
        
        await cwr.Start();
        
        Console.WriteLine("Конец MAIN");
        
    }
}