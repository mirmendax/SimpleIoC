using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Model
{
    public class ModelOne
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} \n" +
                $"Age: {Age} \n" +
                $"Address: {Address}";
        }
    }
}
