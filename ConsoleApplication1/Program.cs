using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace ConsoleApplication1
{
    
    class Program
    {
        static void Main(string[] args)
        {
     
            Mutex mx = new Mutex(false, "waite");
            Mutex.OpenExisting("waite");
            mx.ReleaseMutex();
            mx.WaitOne();
       
            Console.ReadLine();
            
        }
        public class F
        {
            public void Print()
            {
                Thread.Sleep(5000);
                Console.WriteLine(Thread.CurrentThread.Name);

            }
        }


    }
}
