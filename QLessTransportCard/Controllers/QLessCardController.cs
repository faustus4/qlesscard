using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLessTransportCard.Services;
using QLessTransportCard.Domain;
using QLessTransportCard.Data;
using QLessTransportCard.Models;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace QLessTransportCard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QLessCardController : ControllerBase
    {
        private readonly QLessCardService _qLessCardService;
        private readonly QLessCardContext _context;

        public QLessCardController(QLessCardService qLessCardService, QLessCardContext context)
        {
            _qLessCardService = qLessCardService;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QLessCard>> GetQLessCard(int id)
        {
            var qLessCard = await _context.QLessCards.FindAsync(id);

            if (qLessCard == null)
            {
                return NotFound();
            }

            return qLessCard;
        }

        [HttpPost]
        [Route("ride/entry/{id}")]
        public async Task<IActionResult> RideEntry(int id)
        {
            try
            {
                var qLessCard = _context.QLessCards.Find(id);

                if (qLessCard == null)
                {
                    ModelState.AddModelError("ID", "Q-Less card ID dont exist.");
                    return BadRequest(ModelState);
                }

                var qLessCardType = _context.QLessCardTypes.Find(qLessCard.CardType);

                decimal fare = 0;
                decimal additionalDiscount = 0;
                decimal discount = qLessCardType.BaseDiscount;

                // Get last ride
                var lastRide = _context.Rides.Where(q => q.QLessCardID == id).OrderByDescending(q => q.EntryDateTime).FirstOrDefault();

                
                if (lastRide != null)
                {
                    /*if (lastRide.ExitDateTime == DateTime.MinValue)
                    {
                        ModelState.AddModelError("ID", $"Haven't exited last ride yet");
                        return BadRequest(ModelState);
                    }*/

                    if (DateTime.Now.AddYears(-qLessCardType.YearsValid) > lastRide.EntryDateTime)
                    {
                        ModelState.AddModelError("ID", $"Q-Less card already expired");
                        return BadRequest(ModelState);
                    }

                    var dateNow = DateTime.Now.Date;
                    var numberOrRidesToday = _context.Rides.Where(q => q.QLessCardID == id && q.EntryDateTime > dateNow && q.EntryDateTime < dateNow.AddDays(1)).Count();

                    if (numberOrRidesToday > 0 && numberOrRidesToday <= qLessCardType.AdditionalDiscountMaxUsage)
                    {
                        discount += additionalDiscount;
                    }
                }


                if (qLessCardType.BaseDiscount > 0)
                {
                    fare = qLessCardType.BaseFare - (qLessCardType.BaseFare * discount);
                }

                if (qLessCard.Balance < fare)
                {
                    ModelState.AddModelError("ID", $"Q-Less card dont have enough balance. Remaining balance {qLessCard.Balance}");
                    return BadRequest(ModelState);
                }

                var ride = new Ride
                {
                    QLessCardID = id,
                    EntryDateTime = DateTime.Now,
                    Fare = fare
                };

                _context.Rides.Add(ride);

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
            "Error processing data");
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<QLessCard>> Post([FromBody] NewCardRequestModel model)
        {
            try
            {
                var customerId = new CustomerID
                {
                    ID = 0
                };

                if (model == null)
                {
                    return BadRequest();
                }

                var cardType = _context.QLessCardTypes.Where(q => q.ID == model.CardType).FirstOrDefault();

                if (cardType == null)
                {
                    ModelState.AddModelError("Card Type", "Please use valid card type. 1 for normal 2 for discounted");
                    return BadRequest(ModelState);
                }

                // Check if card type is discounted
                if (cardType.BaseDiscount > 0)
                {
                    // If yes Require ID
                    if (model.IDType == 0)
                    {
                        ModelState.AddModelError("ID Type", "ID Type is required for discounted type card");
                        return BadRequest(ModelState);
                    }

                    var idType = _context.IDTypes.Where(q => q.ID == model.IDType).FirstOrDefault();

                    if (idType == null)
                    {
                        ModelState.AddModelError("ID Type", "ID Type is not acceptable, use 1 for senior citizen card and 2 for PWD");
                        return BadRequest(ModelState);
                    }

                    if (String.IsNullOrEmpty(model.IDNumber))
                    {
                        ModelState.AddModelError("ID Number", "ID Number is required for discounted type card");
                        return BadRequest(ModelState);
                    }

                    Regex rx = new Regex(idType.RegexRule,
                            RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    if (!rx.IsMatch(model.IDNumber))
                    {
                        ModelState.AddModelError("ID Number", $"ID Number format is invalid for the type {idType.IDDescription}, please follow this format {idType.PatternFormat}");
                        return BadRequest(ModelState);
                    }

                    customerId = new CustomerID
                    {
                        IDType = model.IDType,
                        IDNumber = model.IDNumber
                    };

                    _context.CustomerIDs.Add(customerId);
                    _context.SaveChanges();
                }

                var qLessCard = new QLessCard
                {
                    DateCreated = DateTime.Now,
                    Balance = cardType.StartingBalance,
                    CardType = model.CardType,
                    CustomerID = customerId.ID
                };

                _context.QLessCards.Add(qLessCard);

                _context.SaveChanges();

                return CreatedAtAction(
                    nameof(GetQLessCard),
                    new { id = qLessCard.ID },
                    qLessCard);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
            "Error processing data");
            }
        }
        
    }
}
