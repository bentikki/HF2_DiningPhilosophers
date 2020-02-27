using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    class Philosopher
    {
        public string Name { get; private set; }
        public Fork LeftFork { get; private set; }
        public Fork RightFork { get; private set; }
        public TimeSpan TimeToDie { get; } = TimeSpan.FromSeconds(10);
        public int EatTime { get; } =  Convert.ToInt16(TimeSpan.FromSeconds(1).TotalMilliseconds);
        private int starveCounter;
        private Random random = new Random();

        public Philosopher(string name, Fork leftFork, Fork rightFork)
        {
            Name = name;
            LeftFork = leftFork;
            RightFork = rightFork;
        }


        public void Eat()
        {
            while (IsAlive())
            {
                //Try to get left Fork.
                if (Monitor.TryEnter(LeftFork))
                {
                    //You get left fork.
                    Monitor.Enter(LeftFork);
                    try
                    {
                        Console.WriteLine($"{Name} took LeftFork({LeftFork.Name}).");

                        //Try to get right fork.
                        if (Monitor.TryEnter(RightFork))
                        {
                            //You get right fork.
                            Monitor.Enter(RightFork);
                            try
                            {
                                Console.WriteLine($"{Name} took RightFork({RightFork.Name}).");
                                //Eat
                                Console.WriteLine($"....................{Name} is eating with LeftFork({LeftFork.Name}) & RightFork({RightFork.Name}).");
                                Thread.Sleep(EatTime);
                                Console.WriteLine($"....................{Name} is done eating.");

                            }
                            finally
                            {
                                //Release both forks.
                                Monitor.Exit(RightFork);
                                Monitor.Exit(LeftFork);
                                this.starveCounter = 0;
                                Console.WriteLine($"{Name} just ate. Released all forks({LeftFork.Name})&({RightFork.Name}) and is thinking..");
                            }
                        }
                        else
                        {
                            //You dont get right fork, so release left fork.
                            Monitor.Exit(LeftFork);
                            // Wait / think
                            Console.WriteLine($"{Name} got LeftFork({LeftFork.Name}) but not RightFork({RightFork.Name}), is thinking..");
                            Think();
                        }

                    }
                    finally
                    {
                        Monitor.Exit(LeftFork); //Exeption
                        Think();
                    }
                }
                else
                {
                    //You dont get LeftFork fork, so think.
                    // Wait / think
                    Console.WriteLine($"{Name} didnt get LeftFork({LeftFork.Name}), is thinking..");
                    Think();
                }
            }

            Console.WriteLine($".........................................{Name} died of starvation.");
            
        }

        private void Think()
        {
            Console.WriteLine($"{Name} is thinking.");
            int sleepTime = random.Next(1000, 2000);
            Thread.Sleep(sleepTime);
            this.starveCounter += sleepTime;
        }

        private bool IsAlive()
        {
            if (this.starveCounter >= TimeToDie.TotalMilliseconds)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool TakeForks()
        {
            bool hasForks = false;
            if (Monitor.TryEnter(LeftFork))
            {
                Monitor.Enter(LeftFork);
                try
                {
                    if (Monitor.TryEnter(RightFork))
                    {
                        Monitor.Enter(RightFork);
                        try
                        {
                            hasForks = true;
                        }
                        finally
                        {
                            Monitor.Exit(RightFork);
                        }
                    }

                }
                finally
                {
                    Monitor.Exit(LeftFork);
                }
            }
            return hasForks;
        }

    }
}
