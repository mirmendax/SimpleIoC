using ConsoleApp.Interface;
using ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleIoC.Attributes;

namespace ConsoleApp.Rules
{
    [SimpleBinder(Service:typeof(IRuleOrderConvert<ModelOne, ModelTwo>), Name = "AddressRule")]
    public class AddressRule : IRuleOrderConvert<ModelOne, ModelTwo>
    {
        public int OrderRule => 2;

        public virtual void Convert(ModelOne source, ModelTwo dest)
        {
            if (dest != null && !string.IsNullOrEmpty(source?.Address))
            {
                SetParam(dest, source.Address.Split(',').Select(s => s.Trim()).ToArray());
            }
        }

        private void SetParam(ModelTwo dest, string[] address)
        {
            dest.City = address[0];
            dest.Streat = address[1];
            int.TryParse(address[2], out int value);
            dest.HouseNumber = value;
        }
    }
}
