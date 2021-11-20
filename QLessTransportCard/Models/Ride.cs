using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLessTransportCard.Models
{
    public class Ride
    {
        public int ID { get; set; }
        public int QLessCardID { get; set; }
        public DateTime EntryDateTime { get; set; }
        public DateTime ExitDateTime { get; set; }
        public decimal Fare { get; set; }
    }
}
