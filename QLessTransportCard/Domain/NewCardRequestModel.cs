using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLessTransportCard.Domain
{
    public class NewCardRequestModel
    {
        public int CardType { get; set; }
        public int IDType { get; set; }
        public string IDNumber { get; set; }
    }
}
