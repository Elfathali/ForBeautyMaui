using System;
namespace ForBeauty.Models
{
	public class CheckCoupon
	{
		public int Id { get; set; }
		public string code { get; set; }
		public int UserId { get; set; }
		public double discount { get; set; }
		public int totalLimt { get; set; }
		public string couponeMessage { get; set; }
	}
}

