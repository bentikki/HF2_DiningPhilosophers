using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    class Fork
    {

        public string Name { get; set; }
        public int Number { get; set; }

        public Fork(int number, string name)
        {
            Name = name;
            Number = number;
        }
    }
}
