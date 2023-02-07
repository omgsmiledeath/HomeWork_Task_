using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Test
{
    public class ConsoleWriter
    {
        int threadCount;
        List<string> messageBook;
        object _obj;
        Queue<ConsoleColor> qc;
        public ConsoleWriter(int threadCount)
        {
            this.threadCount = threadCount;
            messageBook = new List<string>();
            qc = new Queue<ConsoleColor>();
            _obj = new object();
        }

        public void SetMessage(string mess)
        {
            messageBook.Add(mess);
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
                string mess = messageBook[new Random().Next(0, messageBook.Count)];
                
                   // Console.ForegroundColor = qc.Dequeue();
                    
                    Console.WriteLine($"{mess} in {Thread.CurrentThread.ManagedThreadId} thread");
                   // Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(sleep);
                Console.WriteLine($"End Write Method {mess}");
            }
            );
        }
        public void Start()
        {
            for (int i = 0; i < threadCount; i++)
                Write();
        }
    }
}
