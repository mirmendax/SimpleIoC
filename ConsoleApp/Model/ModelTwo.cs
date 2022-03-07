using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Model
{
    public class ModelTwo
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }

        public int? Age { get; set; }

        public bool IsOld { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Streat { get; set; }
        public int? HouseNumber { get; set; }


        public override string ToString()
        {
            return $"FirstName: {FirstName} MiddleName: {MiddleName} SurName: {SurName} \n" +
                $"Age: {Age} \n" +
                $"Old: {IsOld}\n" +
                $"Country: {Country} \n" +
                $"City: {City}, Streat: {Streat}, HouseNumber: {HouseNumber}";
        }
    }
}
