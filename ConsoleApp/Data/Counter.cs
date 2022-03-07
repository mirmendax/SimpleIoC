using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Data
{
    public class Counter
    {
        public int Count { get; set; } = 0;

        public void Inc()
        {
            Count++;
        }
    }
}
