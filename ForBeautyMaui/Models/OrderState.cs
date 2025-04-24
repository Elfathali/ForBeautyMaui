using System;
namespace ForBeauty.Models
{
	public class OrderState
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int StepSelected { get; set; }
		public int OrderId { get; set; }
		public string TypeOfOrder { get; set;}
	}
}

