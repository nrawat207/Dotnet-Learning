using System;
using System.Threading;

namespace Multithreading_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Start new Thread using 'ThreadStart'
            new Thread(new ThreadStart(ExecuteLongRunningOperation)).Start();

            //2.Start new Thread using 'ParameterizedThreadStart'
            new Thread(new ParameterizedThreadStart(ExecuteLongRunningOperation)).Start(1000);

            //3. Run backround service using thread
            IService service = new EmailService("Email");
            new Thread(new ParameterizedThreadStart
            (RunBackgroundService)).Start(service);

            Console.WriteLine("Main thread");
        }

        static void ExecuteLongRunningOperation()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Operation completed successfully, thread 1");
        }

        static void ExecuteLongRunningOperation(object milliseconds)
        {
            Thread.Sleep((int)milliseconds);
            Console.WriteLine("Operation completed successfully thread 2");
        }

        static void RunBackgroundService(Object service)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;  //set the thread priority
            Thread.Sleep(1000);
            ((IService)service).Execute(); //Long running task 
        }
    }

    public interface IService
    {
        string Name { get; set; }
        void Execute();
    }

    public class EmailService : IService
    {
        public string Name { get; set; }
        public void Execute() => Console.WriteLine("Email Service executed successfully");

        public EmailService(string name)
        {
            this.Name = name;
        }
    }
}
