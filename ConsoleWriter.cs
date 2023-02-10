using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Test
{
    public class ConsoleWriter
    {
        int threadCount; //Количество потоков
        Queue<string> messageBook; //Коллекция сообщений для вывода в консоль
        private object _dumb ;//для Lock()
        Queue<ConsoleColor> qc;//Коллекция цветов сообщений

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="threadCount">Количество Сообщений</param>
        public ConsoleWriter(int threadCount)
        {
            this.threadCount = threadCount;
            messageBook = new Queue<string>();
            qc = new Queue<ConsoleColor>();
            _dumb = new Object();
        }

        /// <summary>
        /// Метод для записи сообщений в коллекцию messageBook
        /// </summary>
        /// <param name="mess">Сообщение</param>
        public void SetMessage(string mess)
        {
            messageBook.Enqueue(mess);
        }
        /// <summary>
        /// Метод записи цветов Console в коллекцию qc
        /// </summary>
        /// <param name="cc"></param>
        public void setColor(ConsoleColor cc)
        {
            qc.Enqueue(cc);
        }
        /// <summary>
        /// Метод для вывода сообщений в консоль с случайным цветом
        /// </summary>
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
        /// <summary>
        /// Главный метод,запускающий Write() в заданном количестве потоков
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
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
