using System;
using System.Collections.Generic;

namespace ForBeauty.Models
{
    public class Order
    {
        public int id { get; set; }
        public string TypeOfOrder { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Coupon { get; set; }
        public double TotalAmount { get; set; }
        public int UserId { get; set; }
        public DateTime orderPlaced { get; set; }

    }
}


