using ConsoleApp.Interface;
using ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Rules
{    
    public class AfterRuleAge 
    {
        private int OldAge = 50;

        public AfterRuleAge()
        {

        }

        public AfterRuleAge(int OldAge)
        {
            this.OldAge = OldAge;
        }

        public void Convert(ModelTwo dest)
        {
            if (dest != null && dest.Age != default && dest.Age > OldAge)
                dest.IsOld = true;
        }
    }
}
