using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncUsingMonitor
{
    public class JobExecutor
    {
        const int _waitTimeInMillis = 10 * 60 * 1000;
        private ArrayList _jobs = null;
        private static JobExecutor _instance = null;
        private static object _syncRoot = new object();

        //Singleton implementation of JobExecutor
        public static JobExecutor Instance
        {
            get
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new JobExecutor();
                }
                return _instance;
            }
        }

        private JobExecutor()
        {
            IsIdle = true;
            IsAlive = true;
            _jobs = new ArrayList();
        }

        private Boolean IsIdle { get; set; }
        public Boolean IsAlive { get; set; }

        //Callers can use this method to add list of jobs
        public void AddJobItems(List<Job> jobList)
        {
            //Added lock to provide synchronous access. 
            //Alternatively we can also use Monitor.Enter and Monitor.Exit
            lock (_jobs)
            {
                foreach (Job job in jobList)
                {
                    _jobs.Add(job);
                }
                //Release the waiting thread to start executing the //jobs
                Monitor.PulseAll(_jobs);
            }
        }

        /*Check for jobs count and if the count is 0, then wait for 10 minutes by calling Monitor.Wait. Meanwhile, if new jobs are added to the list, Monitor.PulseAll will be called that releases the waiting thread. Once the waiting is over it checks the count of jobs and if the jobs are there in the list, start executing. Otherwise, wait for the new jobs */
        public void CheckandExecuteJobBatch()
        {
            lock (_jobs)
            {
                while (IsAlive)
                {
                    if (_jobs == null || _jobs.Count <= 0)
                    {
                        IsIdle = true;
                        Console.WriteLine("Now waiting for new jobs");
                        //Waiting for 10 minutes 
                        Monitor.Wait(_jobs, _waitTimeInMillis);
                    }
                    else
                    {
                        IsIdle = false;
                        ExecuteJob();
                    }
                }
            }
        }

        //Execute the job
        private void ExecuteJob()
        {
            for (int i = 0; i < _jobs.Count; i++)
            {
                Job job = (Job)_jobs[i];
                //Execute the job; 
                job.DoSomething();
                //Remove the Job from the Jobs list 
                _jobs.Remove(job);
                i--;
            }
        }
    }
}
