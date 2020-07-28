using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("Data")]
    [ApiController]
    public class DataController : Controller
    {
        private UmbrellaContext db;

        public DataController(UmbrellaContext context)
        {
            db = context;
        }

        [HttpGet("GetRatePlan")]
        public List<RateItem> GetRatePlan()
        {

            Console.WriteLine("RATE_PLAN " + UmbrellaContext.RATE_PLAN_ID);

            List<RateItem> rateItems = db.Rates
                .Where(r => r.RatePlan.RatePlanId == UmbrellaContext.RATE_PLAN_ID)
                .OrderBy(r => r.Price)
                .ToList();
            //.FirstOrDefault();


            Console.WriteLine("RRRRRRRRRRRRATES " + rateItems.Count); 
                Console.WriteLine("DAAAATEEETIIIMEEEE " + new DateTime());
            /*RatePlan ratePlan = db.RatePlans
                .Where(r => r.RatePlanId == UmbrellaContext.RATE_PLAN_ID)
                .FirstOrDefault();*/

            return rateItems;
        }

        [HttpGet("AddUser")]
        public async Task<UserIdentificator> AddUser()
        {
            User createdUser = new User() { RegisterDate = new DateTime() };

            db.Users.Add(createdUser);

            await db.SaveChangesAsync();

            return new UserIdentificator() { UserId = createdUser.UserId };
        }

        [HttpGet("GetFaqs")]
        public async Task<List<Faq>> GetFaqs()
        {

            Console.WriteLine("FFFFAAAAAAAAAAAAAQQQQQQQQQQQQQ");


            List<Faq> faqs = db.Faqs.ToList();
            Console.WriteLine(faqs.Count());

            return faqs;
        }

        [HttpGet("GetPoints")]
        public List<Point> GetPoints()
        {
            List<Point> points = db.Points.ToList();
            Console.WriteLine(points.Count());

            return points;
        }
    }
}