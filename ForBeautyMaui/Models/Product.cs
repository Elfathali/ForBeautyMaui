 using System;
namespace ForBeauty.Models
{
	public class Product
	{
        public int Id { get; set; }
        public string name { get; set; }
        public object image { get; set; }
        public string imageUrl { get; set; }
        public int price { get; set; }
        public int discount { get; set; }
        public bool isProductAvalabile { get; set; }
        public string categoryName { get; set; }
        public bool showInGifts { get; set; }
        public string ProductDiscription { get; set; }
        public string Size { get; set; }
        public int categoryId { get; set; }
        public string MoreDital { get; set; }
        public string ImageUrlForGift { get; set;}

    }
}