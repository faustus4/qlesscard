using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLessTransportCard.Data;
using QLessTransportCard.Models;
using QLessTransportCard.Domain;

namespace QLessTransportCard.Services
{
    public class QLessCardService
    {
        private readonly QLessCardContext  _qLessCardContext;

        public QLessCardService(QLessCardContext qLessCardContext)
        {
            _qLessCardContext = qLessCardContext;
        }

        public int CreateQLessCard(NewCardRequestModel model)
        {
            int response = 0;
            try
            {
                var cardType = _qLessCardContext.QLessCardTypes.Where(q => q.ID == model.CardType).FirstOrDefault<QLessCardType>();

                var newCard = new QLessCard
                {
                    DateCreated = DateTime.Now,
                    CardType = model.CardType,
                    Balance = cardType.StartingBalance
                };

                _qLessCardContext.QLessCards.Add(newCard);

                _qLessCardContext.SaveChanges();

                response = newCard.ID;
            }
            catch (Exception ex)
            {

            }

            return response;
        }
    }
}
