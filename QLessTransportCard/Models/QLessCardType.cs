using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLessTransportCard.Models
{
    public class QLessCardType
    {
        public int ID { get; set; }
        public string CardType { get; set; }
        public decimal BaseFare { get; set; }
        public decimal BaseDiscount { get; set; }
        public decimal AdditionalDiscount { get; set; }
        public int AdditionalDiscountMaxUsage { get; set; }
        public decimal MinimumReload { get; set; }
        public decimal MaximumReload { get; set; }
        public decimal MaximumBalance { get; set; }
    }
}
