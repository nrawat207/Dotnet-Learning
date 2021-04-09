using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncUsingMonitor
{
    public class Job
    {
        // Properties to set and get Job ID and Name
        public int JobID { get; set; }
        public string JobName { get; set; }

        //Do some task based on Job ID as set through the JobID        
        //property
        public void DoSomething()
        {
            //Do some task based on Job ID  
            Console.WriteLine("Executed job " + JobID);
        }
    }
}
