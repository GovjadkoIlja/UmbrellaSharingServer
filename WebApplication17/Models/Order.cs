using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public abstract class Order
    {
        public User OrderedUser { get; set; }
        public DateTime GetCodeTime { get; set; }
        public DateTime TakeTime { get; set; }
        public int Price { get; set; }
    }
}
