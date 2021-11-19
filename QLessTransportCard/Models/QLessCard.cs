using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLessTransportCard.Models
{
    public class QLessCard
    {
        public int ID { get; set; }
        public DateTime DateCreated { get; set; }
        public int CardType { get; set; }
        public int CustomerID { get; set; }
        public decimal Balance { get; set; }
    }
}
