using EagleCore;

namespace EagleServices
{
	public interface IBotStorage
	{
		void AddLocation(BotRecord record);
		List<BotRecord> GetCurrentLocations();
	}
}