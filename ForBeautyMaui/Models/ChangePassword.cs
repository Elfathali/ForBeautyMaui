using System;
namespace ForBeauty.Models
{
	public class ChangePassword
	{
        public string PhoneNumber { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

