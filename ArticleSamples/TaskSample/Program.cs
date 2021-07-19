using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSample
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            //Task task = Task.Factory.StartNew(() => {
            //    // Just loop.
            //    int ctr = 0;
            //    for (ctr = 0; ctr <= 1000000; ctr++)
            //    { }
            //    Console.WriteLine("Finished {0} loop iterations",
            //                      ctr);
            //});

            //task.Wait();


            //WaitAny
            //Wait any task to complete
            //var tasks = new Task[3];
            //var rnd = new Random();
            //for (int ctr = 0; ctr <= 2; ctr++)
            //    tasks[ctr] = Task.Run(() => Thread.Sleep(rnd.Next(200, 3000)));

            //try
            //{
            //    int index = Task.WaitAny(tasks);
            //    Console.WriteLine("Task #{0} completed first.\n", tasks[index].Id);
            //    Console.WriteLine("Status of all tasks:");
            //    foreach (var t in tasks)
            //        Console.WriteLine("   Task #{0}: {1}", t.Id, t.Status);
            //}
            //catch (AggregateException)
            //{
            //    Console.WriteLine("An exception occurred.");
            //}

            ///////////////////////////////////////////////
            // Wait for all tasks to complete.
            //Task[] tasks = new Task[10];
            //for (int i = 0; i < 10; i++)
            //{
            //    tasks[i] = Task.Run(() => Thread.Sleep(2000));
            //}
            //try
            //{
            //    Task.WaitAll(tasks);
            //}
            //catch (AggregateException ae)
            //{
            //    Console.WriteLine("One or more exceptions occurred: ");
            //    foreach (var ex in ae.Flatten().InnerExceptions)
            //        Console.WriteLine("   {0}", ex.Message);
            //}

            //Console.WriteLine("Status of completed tasks:");
            //foreach (var t in tasks)
            //    Console.WriteLine("   Task #{0}: {1}", t.Id, t.Status);


            Task[] tasks = new Task[2];
            String[] files = null;
            String[] dirs = null;
            String docsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            tasks[0] = Task.Factory.StartNew(() => files = Directory.GetFiles(docsDirectory));
            tasks[1] = Task.Factory.StartNew(() => dirs = Directory.GetDirectories(docsDirectory));

            await Task.Factory.ContinueWhenAll(tasks, completedTasks =>
            {
                Console.WriteLine("{0} contains: ", docsDirectory);
                Console.WriteLine("   {0} subdirectories", dirs.Length);
                Console.WriteLine("   {0} files", files.Length);
            });
        }
    }
}
