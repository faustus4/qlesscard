using QLessTransportCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLessTransportCard.Data
{
    public static class DbInitializer
    {
        public static void Initialize(QLessCardContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.QLessCardTypes.Any())
            {
                return;   // DB has been seeded
            }

            var qLessCardTypes = new QLessCardType[]
            {
                new QLessCardType{CardType = "Normal", BaseFare = 15, BaseDiscount = 0, AdditionalDiscount = 0, AdditionalDiscountMaxUsage = 0, MinimumReload = 100, MaximumReload = 1000, MaximumBalance = 10000},
                new QLessCardType{CardType = "Discounted", BaseFare = 10,  BaseDiscount = 0.2M, AdditionalDiscount = 0.03M, AdditionalDiscountMaxUsage = 4, MinimumReload = 100, MaximumReload = 1000, MaximumBalance = 10000}
            };

            foreach (QLessCardType qLessCardType in qLessCardTypes)
            {
                context.QLessCardTypes.Add(qLessCardType);
            }
            context.SaveChanges();

        }
    }
}
