using ConsoleApp.Interface;
using ConsoleApp.Model;
using SimpleIoC.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Rules
{
    //[DIBinder(Service: typeof(IRuleOrderConvert<ModelOne, ModelTwo>), Name = "AgeRule")]
    public class AgeRule : IRuleOrderConvert<ModelOne, ModelTwo>
    {
        public int OrderRule => 3;

        public void Convert(ModelOne source, ModelTwo dest)
        {
            if (dest != null && source?.Age != default)
                dest.Age = source.Age;
        }
    }
}
