using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class RateItem
    {
        public int RateItemId { get; set; }
        public RatePlan RatePlan { get; set; }
        public TimeSpan MinTime { get; set; }
        public TimeSpan MaxTime { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsBuy { get; set; }
    }
}
