﻿using System;
namespace ForBeauty.Models
{
	public class TokenReponse
	{
		public string access_token { get; set; }
		public string token_type { get; set; }
		public int user_Id { get; set; }
		public string user_name { get; set; }
		public int expires_in { get; set; }
        public int creation_Time { get; set; }
        public int expiration_Time { get; set;}

    }
}

