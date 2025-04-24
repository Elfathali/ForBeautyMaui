using System;
namespace ForBeautyMaui
{
	public class UnixTime
	{
		public static int  GetCurrentTime()
		{
			DateTime dateTime = new DateTime(1970, 1, 1);
			return (int)((DateTime.Now.ToUniversalTime()- dateTime).TotalSeconds + 0.5);
		}
	}
}

