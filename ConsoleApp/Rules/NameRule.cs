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
    [SimpleBinder(Service: typeof(IRuleOrderConvert<ModelOne, ModelTwo>), Name = "NameRule")]
    public class NameRule : IRuleOrderConvert<ModelOne, ModelTwo>
    {
        public int OrderRule => 0;

        public void Convert(ModelOne source, ModelTwo dest)
        {
            if (!string.IsNullOrEmpty(source?.Name) && dest != null)
            {
                SetName(dest, source.Name.Split(' ').Select(x=> x.Trim()).ToArray());
            }
        }

        private void SetName(ModelTwo dest, string[] names)
        {
            dest.FirstName = names[0];
            dest.MiddleName = names[1];
            dest.SurName = names[2];
        }
    }
}
