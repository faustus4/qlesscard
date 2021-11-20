using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLessTransportCard.Models
{
    public class IDType
    {
        public int ID { get; set; }
        public string IDDescription { get; set; }
        public string RegexRule { get; set; }
        public string PatternFormat { get; set; }
    }
}
