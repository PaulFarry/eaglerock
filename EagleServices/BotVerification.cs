using EagleCore;

namespace EagleServices
{
	/// <summary>
	/// </summary>
	/// <remarks>
	/// Perform Verification of the Record. 
	/// Height, Lat, long
	/// Is it one of our bots
	/// </remarks>
	public class BotVerification : IBotVerification
	{
		public bool Verify(BotRecord record)
		{
			return true;
		}
	}
}
