using System;
namespace ForBeauty.Models
{
	public class PrefeuseOrderUserById
	{
        public int id { get; set; }
        public string Address { get; set; }
        public double totalAmount { get; set; }
        public int UserId { get; set; }
        public string orderPlaced { get; set; }
        public object OrderDetail { get; set; }
    }
}

