namespace EagleCore
{

	public class BotRecord
	{
		//Which Bot is sending the information
		public Guid Identifier { get; set; }

		public DateTimeOffset Timestamp { get; set; }
		public string RoadName { get; set; }
		public BotDirection FlowDirection { get; set; }
		public float FlowRate { get; set; }
		public float VehicleSpeedAverage { get; set; }

		public BotLocation Location { get; set; } = BotLocation.Empty;

	}
}
