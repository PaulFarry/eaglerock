using EagleCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace EagleServices
{
	public class BotStorage(IMemoryCache cache) : IBotStorage
	{
		internal readonly ConcurrentDictionary<Guid, BotRecord> botLocations = new();

		public void AddLocation(BotRecord record)
		{
			var bot = record.Identifier;

			var botKey = $"BOTList-{bot:D}";

			var botList = cache.GetOrCreate<List<BotRecord>>(botKey, entry =>
			{
				//TODO: Configuration for this
				entry.SlidingExpiration = TimeSpan.FromDays(1);

				return [];
			});

			botList?.Add(record);

			//We aren't currently dealing with the removal of the bot locations, as this is only in memory, but we should also handle that as a bot may be decommsiioned
			if (botLocations.TryAdd(record.Identifier, record))
			{
				botLocations[record.Identifier] = record;
			}
		}

		public List<BotRecord> GetCurrentLocations()
		{
			var currentLocations = new List<BotRecord>(botLocations.Values);
			return currentLocations;
		}
	}
}
