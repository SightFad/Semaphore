using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreLab06
{
    internal class Program
    {
        private static Semaphore semaphore1 = new Semaphore(1, 1);
        private static Semaphore semaphore2 = new Semaphore(0, 1);
        private static Semaphore semaphore3 = new Semaphore(0, 1);
        private static int a = 6;
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(add);
            Thread thread2 = new Thread(subtract);
            Thread thread3 = new Thread(multi);

            thread1.Start();
            thread2.Start();
            thread3.Start();

            // Đợi các thread hoàn thành
            thread1.Join();
            thread2.Join();
            thread3.Join();

            // Hiển thị giá trị cuối cùng của a
            Console.WriteLine($"Final value of a: {a}");
            Console.ReadLine();
        }
        static void add()
        {
            semaphore2.Release();
            semaphore1.WaitOne();
            semaphore1.WaitOne();
            a = a + 7;
            Console.WriteLine("Thread 1: " + a);

        }
        static void subtract()
        {
            semaphore2.WaitOne();
            a = a - 5;
            Console.WriteLine("Thread 2: " + a);
            semaphore3.Release();

        }
        static void multi()
        {
            semaphore3.WaitOne();
            a = a * 3;
            Console.WriteLine("Thread 3: " + a);
            semaphore1.Release();
            semaphore1.Release();
        }
    }
}
