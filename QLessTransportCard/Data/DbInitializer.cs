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
                new QLessCardType{CardType = "Normal", BaseFare = 15, BaseDiscount = 0, AdditionalDiscount = 0, AdditionalDiscountMaxUsage = 0, MinimumReload = 100, MaximumReload = 1000, MaximumBalance = 10000, StartingBalance = 100, YearsValid = 5},
                new QLessCardType{CardType = "Discounted", BaseFare = 10,  BaseDiscount = 0.2M, AdditionalDiscount = 0.03M, AdditionalDiscountMaxUsage = 4, MinimumReload = 100, MaximumReload = 1000, MaximumBalance = 10000, StartingBalance = 500, YearsValid = 3}
            };

            foreach (QLessCardType qLessCardType in qLessCardTypes)
            {
                context.QLessCardTypes.Add(qLessCardType);
            }
            context.SaveChanges();

            var iDTypes = new IDType[]
            {
                new IDType{IDDescription = "Senior Citizen", RegexRule = "[0-9a-z]{2}-[0-9a-z]{4}-[0-9a-z]{4}", PatternFormat = "##-####-####"},
                new IDType{IDDescription = "PWD", RegexRule = "[0-9a-z]{4}-[0-9a-z]{4}-[0-9a-z]{4}", PatternFormat = "####-####-####"}
            };

            foreach (IDType iDType in iDTypes)
            {
                context.IDTypes.Add(iDType);
            }
            context.SaveChanges();

        }
    }
}
