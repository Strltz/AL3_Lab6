using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public struct Weather
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public double Temp { get; set; }
        public string Description { get; set; }

        public Weather(string country, string name, double temp,
            string description)
        {
            Country = country;
            Name = name;
            Temp = temp;
            Description = description;
        }
    }
}
