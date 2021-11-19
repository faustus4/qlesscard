using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLessTransportCard.Data;
using QLessTransportCard.Models;

namespace QLessTransportCard.Services
{
    public class QLessCardService
    {
        private readonly QLessCardContext  _qLessCardContext;

        public QLessCardService(QLessCardContext qLessCardContext)
        {
            _qLessCardContext = qLessCardContext;
        }

        public void CreateQLessCard()
        {
            try
            {
                var newCard = _qLessCardContext.QLessCards.Add(
                new QLessCard { DateCreated = DateTime.Now, CardType = 1, Balance = 50 }
                );

                _qLessCardContext.SaveChanges();
                var test = "hey";
            }
            catch
            {

            }
        }
    }
}
