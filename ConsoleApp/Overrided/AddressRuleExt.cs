using ConsoleApp.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleIoC.Attributes;
using ConsoleApp.Model;

namespace ConsoleApp.Overrided
{
    [SimpleOverrided]
    public class AddressRuleExt : AddressRule
    {
        public override void Convert(ModelOne source, ModelTwo dest)
        {
            if (dest != null && !string.IsNullOrEmpty(source?.Address))
            {
                SetParam(dest, source.Address.Split(',').Select(s => s.Trim()).ToArray());
            }
        }

        private void SetParam(ModelTwo dest, string[] address)
        {
            dest.City = address[0]+" Ext";
            dest.Streat = address[1] + " Ext";
            int.TryParse(address[2], out int value);
            dest.HouseNumber = value;
        }
    }
}
