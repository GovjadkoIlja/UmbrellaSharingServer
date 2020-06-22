using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("Home")]
    [ApiController]
    public class HomeController : Controller
    {
        private UmbrellaContext db;
       

        public HomeController(UmbrellaContext context)
        {
            db = context;


        }

        [HttpGet]
        public String Index()
        {
            return "ПРИВЕТ МИР!";
        }

        [HttpGet("FillDb")]
        public void FillDb()
        {
            //fillDb();

            /*foreach (Faq faq in db.Faqs)
                db.Faqs.Remove(faq);

            foreach (RateItem rate in db.Rates)
                db.Rates.Remove(rate);

            foreach (RatePlan ratePlans in db.RatePlans)
                db.RatePlans.Remove(ratePlans);*/

            db.Faqs.Add(new Faq() { Question = "Выдали сломанный зонт", Answer = "Вернитесь к стойке выдачи зонтов, туда, где брали зонт. Если Вы это сделаете в течение 5 минут после получения зонта — мы выдадим Вам новый зонт" });
            db.Faqs.Add(new Faq() { Question = "По пути сломался зонт", Answer = "Ни в коем случае не выбрасывайте зонт и не доламывайте его. Просто дойдите до точки сдачи зонтов и мы посмотрим, что случилось" });
            db.Faqs.Add(new Faq() { Question = "Поменялись планы и не могу сегодня вернуть зонт", Answer = "Ничего страшного. У нас действует тарифная сетка, позволяющая вернуть зонт с промежутком времени до суток. А если вы берете зонт больше, чем на сутки, то он и вовсе достается вам навсегда, а стоит это будет 300 рублей" });
            //await db.SaveChangesAsync();

            RatePlan rp = new RatePlan();

            db.RatePlans.Add(rp);
            //await db.SaveChangesAsync();
            db.SaveChanges();

            List<RateItem> rateItems = new List<RateItem>();

            db.Rates.Add(new RateItem() { RatePlan = rp, Description = "Менее часа", MinTime = new TimeSpan(0), MaxTime = new TimeSpan(1, 0, 0), Price = 50, IsBuy = false });
            db.Rates.Add(new RateItem() { RatePlan = rp, Description = "Менее дня", MinTime = new TimeSpan(1, 0, 0), MaxTime = new TimeSpan(23, 59, 59), Price = 100, IsBuy = false });
            db.Rates.Add(new RateItem() { RatePlan = rp, Description = "Больше дня и зонт ваш", MinTime = new TimeSpan(23, 59, 59), MaxTime = new TimeSpan(), Price = 300, IsBuy = false });
            db.Rates.Add(new RateItem() { RatePlan = rp, Description = "Купить зонт", MinTime = new TimeSpan(0), MaxTime = new TimeSpan(0), Price = 300, IsBuy = true });

            //await db.SaveChangesAsync();
            db.SaveChanges();

            db.Points.Add(new Point() { Latitude = 55.754724, Longitude = 37.621380, Description = "Точка выдачи и сдачи зонтов" });
            db.Points.Add(new Point() { Latitude = 55.760133, Longitude = 37.618697, Description = "Скидка 30%" });
            db.Points.Add(new Point() { Latitude = 55.764753, Longitude = 37.591313, Description = "Кофе в подарок" });
            db.Points.Add(new Point() { Latitude = 55.728466, Longitude = 37.604155, Description = "Автоматизированный пункт" });

            db.SaveChanges();
            
            Console.WriteLine(" BBBBBBBBBBBBBBBBBBBBBBB " + db.Faqs.Count() + " " + db.RatePlans.Count() + " " + db.Rates.Count() + " " + db.Points.Count());
        }

        [HttpGet("Save")]
        public void Save()
        {
            db.SaveChangesAsync();
        }

        [HttpGet("ClearDb")]
        public void ClearDatabase()
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        private async void fillDb()
        {
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAA " + db.Faqs.Count() + " " + db.RatePlans.Count());
            
            db.Faqs.Add(new Faq() { Question = "Выдали сломанный зонт", Answer = "Вернитесь к стойке выдачи зонтов, туда, где брали зонт. Если Вы это сделаете в течение 5 минут после получения зонта — мы выдадим Вам новый зонт" });
            await db.SaveChangesAsync();
            db.Faqs.Add(new Faq() { Question = "По пути сломался зонт", Answer = "Ни в коем случае не выбрасывайте зонт и не доламывайте его. Просто дойдите до точки сдачи зонтов и мы посмотрим, что случилось" });
            await db.SaveChangesAsync();
            db.Faqs.Add(new Faq() { Question = "Поменялись планы и не могу сегодня вернуть зонт", Answer = "Ничего страшного. У нас действует тарифная сетка, позволяющая вернуть зонт с промежутком времени до суток. А если вы берете зонт больше, чем на сутки, то он и вовсе достается вам навсегда, а стоит это будет 300 рублей" });
            await db.SaveChangesAsync();

            Console.WriteLine("BBBBBBBBBBBBBBBBBBBBBBB " + db.Faqs.Count() + " " + db.RatePlans.Count());
        }

        private async Task fillFaqs(UmbrellaContext db)
        {
            db.Faqs.Add(new Faq() { Question = "Выдали сломанный зонт", Answer = "Вернитесь к стойке выдачи зонтов, туда, где брали зонт. Если Вы это сделаете в течение 5 минут после получения зонта — мы выдадим Вам новый зонт" });
            db.Faqs.Add(new Faq() { Question = "По пути сломался зонт", Answer = "Ни в коем случае не выбрасывайте зонт и не доламывайте его. Просто дойдите до точки сдачи зонтов и мы посмотрим, что случилось" });
            db.Faqs.Add(new Faq() { Question = "Поменялись планы и не могу сегодня вернуть зонт", Answer = "Ничего страшного. У нас действует тарифная сетка, позволяющая вернуть зонт с промежутком времени до суток. А если вы берете зонт больше, чем на сутки, то он и вовсе достается вам навсегда, а стоит это будет 300 рублей" });
            //await SaveChangesAsync();
        }

        private async Task fillRatesAsync(UmbrellaContext db)
        {
            RatePlan rp = new RatePlan();

            db.RatePlans.Add(rp);
            await db.SaveChangesAsync();

            List<RateItem> rateItems = new List<RateItem>();

            db.Rates.Add(new RateItem() { RatePlan = rp, Description = "Менее часа", MinTime = new TimeSpan(0), MaxTime = new TimeSpan(1, 0, 0), Price = 50 });
            db.Rates.Add(new RateItem() { RatePlan = rp, Description = "Менее дня", MinTime = new TimeSpan(1, 0, 0), MaxTime = new TimeSpan(1, 0, 0, 0), Price = 100 });
            db.Rates.Add(new RateItem() { RatePlan = rp, Description = "Больше дня и зонт ваш", MinTime = new TimeSpan(1, 0, 0, 0), MaxTime = new TimeSpan(1000, 0, 0, 0), Price = 250 });
            await db.SaveChangesAsync();

            Console.WriteLine("QQQQQQQQQQQQQQQQQQQ " + db.Rates.Count());

            

            Console.WriteLine("CCCCCCCCCCCCCCCCCCCCC " + db.RatePlans.Count());
        }

        [HttpGet("SendDataTest")]
        public string SendDataTest() //Чисто тестовый метод, используется, чтобы убедиться, что сервер работает и все подключения к нему тоже
        {
            Console.WriteLine("SEND DATA METHOD EXECUTED");
           // hubContext.Clients.All.SendAsync("ReceiveMessage", "TEST MESSAGE");
            return "Send data method executed";
        }
    }
}
