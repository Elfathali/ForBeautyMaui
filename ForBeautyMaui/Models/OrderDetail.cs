using System;
namespace ForBeauty.Models
{
	public class OrderDetail
	{
        public int id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public string Size { get; set; }
        public int Qty { get; set; }
        public int orderId { get; set; }
        public int productId { get; set; }
        public string imageUrl { get; set; }
    }
}

