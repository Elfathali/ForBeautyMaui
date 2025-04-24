using System;
namespace ForBeauty.Models
{
	public class AddToFavourite
	{
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string name { get; set; }
        public string MoreDital { get; set; }
        public string imageUrl { get; set; }
        public int price { get; set; }
        public int Discount { get; set; }
        public string categoryName { get; set; }
        public string discription { get; set; }
        public string Size { get; set; }
    }
}

