namespace EagleCore
{
	public class BotLocation
	{
		public static readonly BotLocation Empty = new() { Altitude = float.MinValue, Longitude = 0, Latitude = 0 };
		public float Latitude { get; set; }
		public float Longitude { get; set; }
		public float Altitude { get; set; }
	}
}
