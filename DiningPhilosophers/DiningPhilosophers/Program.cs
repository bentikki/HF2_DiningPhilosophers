using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    class Program
    {
        static Fork f1 = new Fork(1, "Fork 1");
        static Fork f2 = new Fork(2, "Fork 2");
        static Fork f3 = new Fork(3, "Fork 3");
        static Fork f4 = new Fork(4, "Fork 4");
        static Fork f5 = new Fork(5, "Fork 5");

        static void Main(string[] args)
        {

            Philosopher p1 = new Philosopher("Philosopher 1", f5, f1);
            Philosopher p2 = new Philosopher("Philosopher 2", f1, f2);
            Philosopher p3 = new Philosopher("Philosopher 3", f2, f3);
            Philosopher p4 = new Philosopher("Philosopher 4", f3, f4);
            Philosopher p5 = new Philosopher("Philosopher 5", f4, f5);

            new Thread(p1.Eat).Start();
            new Thread(p2.Eat).Start();
            new Thread(p3.Eat).Start();
            new Thread(p4.Eat).Start();
            new Thread(p5.Eat).Start();

        }
    }
}
