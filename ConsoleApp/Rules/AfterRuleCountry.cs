using ConsoleApp.Model;
using SimpleIoC.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Rules
{
    [SimpleBinder(typeof(AfterRuleCountry), Name = "AfterRuleCountry")]
    public class AfterRuleCountry
    {
        protected Dictionary<string, string> Country { get; set; }

        public AfterRuleCountry()
        {
            Country = new Dictionary<string, string>();
            Country.Add("USA", "New York");
            Country.Add("Russian Federation", "Moscow");
            Country.Add("Japan", "Tokio");
        }

        public void Convert(ModelTwo model)
        {
            if (model.City != null)
            {
                model.Country = FindCountry(model.City);
            }
        }

        public string FindCountry(string city)
        {
            var country = "None";
            foreach (var item in Country)
            {
                if (item.Value.Equals(city))
                    return item.Key;
            }
            return country;
        }

    }
}
