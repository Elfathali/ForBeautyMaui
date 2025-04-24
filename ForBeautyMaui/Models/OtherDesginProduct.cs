using System;
namespace ForBeauty.Models
{
	public class OtherDesginProduct
	{
        public int id { get; set; }
        public string Size { get; set; }
        public int productId { get; set; }
        public int price { get; set; }
        public int Discound { get; set; }
        public bool IsSelected { get; set;}
    }
}

