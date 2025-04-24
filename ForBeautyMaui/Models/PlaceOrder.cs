using System;
using System.Collections.Generic;

namespace ForBeauty.Models
{
    public class PlaceOrder
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double totalAmount { get; set; }
        public int UserId { get; set; }
        public string OrderPlaced { get; set; }


    }
}

