using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Test
{
    public class ConsoleWriter
    {
        int threadCount;
        Queue<string> messageBook;
        private object _dumb ;
        Queue<ConsoleColor> qc;
        public ConsoleWriter(int threadCount)
        {
            this.threadCount = threadCount;
            messageBook = new Queue<string>();
            qc = new Queue<ConsoleColor>();
            _dumb = new Object();
        }

        public void SetMessage(string mess)
        {
            messageBook.Enqueue(mess);
        }
        public void setColor(ConsoleColor cc)
        {
            qc.Enqueue(cc);
        }
        private void Write()
        {
            
                int sleep = new Random(DateTime.Now.Millisecond).Next(1000, 3000);
                Console.WriteLine($"Метод Write() , спит на  {sleep} милисекунд");
                Thread.Sleep(sleep);
                string mess = messageBook.Dequeue();
                
                    lock (_dumb) 
                    {
                    Console.ForegroundColor = qc.Dequeue();
                    
                    Console.WriteLine($"{mess} В {Thread.CurrentThread.ManagedThreadId} thread");
                    Console.ResetColor();
                    }
                Thread.Sleep(sleep);
                Console.WriteLine($"Конец Write() мое сообщение {mess}");
            
            
            
        }
        public async Task Start()
        {
            var tasks = new Task[threadCount];
            await Task.Run(async ()=> 
            {
                System.Console.WriteLine("Start() Начало");
            for (int i = 0; i < threadCount; i++)
                tasks[i]=Task.Run( ()=> Write());
                Task.WaitAll(tasks);
                System.Console.WriteLine("Start() Конец");
               
            });

        }
    }
}
