using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class Rent : Order
    {
        public int RentId { get; set; }
        public DateTime GetPassCodeTime { get; set; }
        public DateTime PassTime { get; set; }
        public int Mark { get; set; }
        public string Feedback { get; set; }
    }
}
