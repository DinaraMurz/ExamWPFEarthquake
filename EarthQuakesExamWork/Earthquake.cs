using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthQuakesExamWork
{
    public class Earthquake
    {
        public DateTime Date { get; set; }
        public double? Depth { get; set; }
        public double? Magnitude { get; set; }
        public string Locality { get; set; }
    }
}
