using System;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizationUsingLock
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var account = new Account(1000);
            var tasks = new Task[100];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => Update(account));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine($"Account's balance is {account.GetBalance()}");
            // Output:
            // Account's balance is 2000
        }

        static void Update(Account account)
        {
            Console.WriteLine($"Current thread Id: {Thread.CurrentThread.ManagedThreadId}");
            decimal[] amounts = { 0, 2, -3, 6, -2, -1, 8, -5, 11, -6 };
            foreach (var amount in amounts)
            {
                if (amount >= 0)
                {
                    account.Credit(amount);
                }
                else
                {
                    account.Debit(Math.Abs(amount));
                }
            }
        }
    }
}
