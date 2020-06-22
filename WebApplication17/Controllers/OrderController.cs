﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("Order")]
    [ApiController]
    public class OrderController : Controller
    {
        private UmbrellaContext db;

        public OrderController(UmbrellaContext context)
        {
            db = context;
        }

        [HttpGet("GetOrderInfo")]
        public int GetOrderInfo(int orderId, int code, int qrType)
        {
            DateTime current = DateTime.Now;
            Order order;

            if (qrType == 1)
            {
                order = db.Buys
                        .Where(r => r.BuyId == orderId)
                        .FirstOrDefault();
            }
            else
            {
                order = db.Rents
                        .Where(r => r.RentId == orderId)
                        .FirstOrDefault();
            }

            Console.WriteLine(order.GetCodeTime.ToString() + " " + code + " " + order.GetCodeTime.ToString().Equals(code) + " " + order.TakeTime + new DateTime());

            if (qrType != 3)
            {
                if (order.GetCodeTime.GetHashCode() != code)
                    return 2; //Неверный код
                else if (!order.TakeTime.Equals(new DateTime()))
                    return 3; //Зонт уже получен
                else
                {
                    order.TakeTime = current;
                    db.SaveChanges();
                    return 1;
                }
            }
            else
            {
                Rent rent = (Rent)order;
                if (rent.GetPassCodeTime.GetHashCode() != code)
                    return 2; //Неверный код
                else if (!rent.PassTime.Equals(new DateTime()))
                    return 3; //Зонт уже получен
                else
                {
                    rent.PassTime = current;
                    db.SaveChanges();
                    return 1;
                }
            }
        }

        [HttpGet("getQrCodeToTake")]
        public async Task<OrderResponse> getQrCodeToTake(int userId, bool isBuy) //Чисто тестовый метод, используется, чтобы убедиться, что сервер работает и все подключения к нему тоже
        {
            DateTime currentTime = DateTime.Now;

            OrderResponse orderResponse = new OrderResponse();

            User user = db.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefault();

            if (isBuy)
            {
                Buy buy = new Buy() { OrderedUser = user, GetCodeTime = currentTime };
                db.Buys.Add(buy);
                await db.SaveChangesAsync();
                orderResponse.OrderId = buy.BuyId;
            }
            else
            {
                Rent rent = new Rent() { OrderedUser = user, GetCodeTime = currentTime };
                db.Rents.Add(rent);
                await db.SaveChangesAsync();
                orderResponse.OrderId = rent.RentId;
            }

            orderResponse.Code = currentTime.GetHashCode();

            Console.WriteLine("QQQQQQQQQQQQQQQQQQQ " + orderResponse.OrderId + " " + orderResponse.Code);

            return orderResponse;
        }

        [HttpGet("getQrCodeToPass")]
        public async Task<int> getQrCodeToPass(int orderId)
        {
            DateTime currentTime = DateTime.Now;


            Rent rent = db.Rents
                    .Where(r => r.RentId == orderId)
                    .FirstOrDefault();

            rent.GetPassCodeTime = currentTime;

            await db.SaveChangesAsync();

            Console.WriteLine("PPPPPAAAAASSSSS " + currentTime.ToString());

            return currentTime.GetHashCode();
        }

        [HttpPost("SaveFeedback")]
        public async Task<int> SaveFeedback(int orderId, string feedback, int mark /*FeedbackData feedbackData*/)
        {

            Console.WriteLine("FEEEDBAAAACK " + orderId + " " + feedback + " " + mark);

            Rent rent = db.Rents
                    .Where(r => r.RentId == orderId)
                    .FirstOrDefault();


            rent.Feedback = feedback;
            rent.Mark = mark;

            await db.SaveChangesAsync();

            return orderId;
        }

        [HttpGet("CanGoFurther")]
        public DateTime CanGoFurther(int orderId, int qrType)
        {
            if (qrType == 1)
            {
                Buy buy = db.Buys
                    .Where(b => b.BuyId == orderId)
                    .FirstOrDefault();

                return buy.TakeTime;
            } else
            {
                Rent rent = db.Rents
                    .Where(r => r.RentId == orderId)
                    .FirstOrDefault();

                Console.WriteLine(rent.TakeTime + " " + rent.PassTime + " " + new DateTime());

                return qrType == 2 ? rent.TakeTime : rent.PassTime;
            }
        }
    }
}