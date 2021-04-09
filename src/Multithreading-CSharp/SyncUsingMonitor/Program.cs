using System;
using System.Collections.Generic;
using System.Threading;

namespace SyncUsingMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread jobThread = new Thread(new ThreadStart(ExecuteJobExecutor));
            jobThread.Start();

            //Starting three Threads add jobs time to time; 
            Thread thread1 = new Thread(new ThreadStart(ExecuteThread1));
            Thread thread2 = new Thread(new ThreadStart(ExecuteThread2));
            Thread thread3 = new Thread(new ThreadStart(ExecuteThread3));
            thread1.Start();
            thread2.Start();
            thread3.Start();

            Console.Read();
        }

        //Implementation of ExecuteThread 1 that is adding three 
        //jobs in the list and calling AddJobItems of a singleton 
        //JobExecutor instance
        private static void ExecuteThread1()
        {
            Thread.Sleep(5000);
            List<Job> jobs = new List<Job>();
            jobs.Add(new Job() { JobID = 11, JobName = "Thread 1 Job 1" });
            jobs.Add(new Job() { JobID = 12, JobName = "Thread 1 Job 2" });
            jobs.Add(new Job() { JobID = 13, JobName = "Thread 1 Job 3" });
            JobExecutor.Instance.AddJobItems(jobs);
        }

        //Implementation of ExecuteThread2 method that is also adding 
        //three jobs and calling AddJobItems method of singleton 
        //JobExecutor instance 
        private static void ExecuteThread2()
        {
            Thread.Sleep(5000);
            List<Job> jobs = new List<Job>();
            jobs.Add(new Job() { JobID = 21, JobName = "Thread 2 Job 1" });
            jobs.Add(new Job() { JobID = 22, JobName = "Thread 2 Job 2" });
            jobs.Add(new Job() { JobID = 23, JobName = "Thread 2 Job 3" });
            JobExecutor.Instance.AddJobItems(jobs);
        }

        //Implementation of ExecuteThread3 method that is again 
        // adding 3 jobs instances into the list and 
        //calling AddJobItems to add those items into the list to execute
        private static void ExecuteThread3()
        {
            Thread.Sleep(5000);
            List<Job> jobs = new List<Job>();
            jobs.Add(new Job() { JobID = 31, JobName = "Thread 3 Job 1" });
            jobs.Add(new Job() { JobID = 32, JobName = "Thread 3 Job 2" });
            jobs.Add(new Job() { JobID = 33, JobName = "Thread 3 Job 3" });
            JobExecutor.Instance.AddJobItems(jobs);
        }

        //Implementation of ExecuteJobExecutor that calls the 
        //CheckAndExecuteJobBatch to run the jobs
        public static void ExecuteJobExecutor()
        {
            JobExecutor.Instance.IsAlive = true;
            JobExecutor.Instance.CheckandExecuteJobBatch();
        }
    }
}
