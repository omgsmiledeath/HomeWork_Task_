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
        object _obj;
        Queue<ConsoleColor> qc;
        public ConsoleWriter(int threadCount)
        {
            this.threadCount = threadCount;
            messageBook = new Queue<string>();
            qc = new Queue<ConsoleColor>();
            _obj = new object();
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
            Task.Run(() =>
            {
                int sleep = new Random(DateTime.Now.Millisecond).Next(1000, 3000);
                Console.WriteLine($"Start Write Method,Sleep from {sleep} milisecond");
                Thread.Sleep(sleep);
                string mess = messageBook.Dequeue();
                
                    Console.ForegroundColor = qc.Dequeue();
                    
                    Console.WriteLine($"{mess} in {Thread.CurrentThread.ManagedThreadId} thread");
                    Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(sleep);
                Console.WriteLine($"End Write Method {mess}");
            }
            );
        }
        public async Task Start()
        {
            await Task.Run(()=> 
            {
            for (int i = 0; i < threadCount; i++)
                Write();
            });
        }
    }
}
