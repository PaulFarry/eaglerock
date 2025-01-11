using EagleCore;

namespace EagleServices
{
	public class BotService(IBotStorage cache, IBotVerification verification)
	{
		public BaseResponse Add(BotRecord record)
		{
			if (verification.Verify(record))
			{
				cache.AddLocation(record);
				return BaseResponse.Successful;
			}

			//We should make the verifycation return the response or message for cleaner separation
			return new BaseResponse { Message = "Verification failed" }; 
		}

		public List<BotRecord> GetAll()
		{
			return cache.GetCurrentLocations();
		}
	}
}
