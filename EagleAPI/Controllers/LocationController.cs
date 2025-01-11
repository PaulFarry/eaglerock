using EagleCore;
using EagleServices;
using Microsoft.AspNetCore.Mvc;

namespace EagleAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LocationController(ILogger<LocationController> logger, BotService botService) : ControllerBase
	{
		/// <summary>
		/// This method receives the records that have been passed from the Drones
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		[HttpPost()]
		public async Task<ActionResult<BaseResponse>> ReceiveLocationRecord(BotRecord record)
		{
			botService.Add(record);
			logger.LogDebug("{Lat} {Long} {Altitude}", record.Location.Latitude, record.Location.Longitude, record.Location.Altitude);
			return await Task.FromResult(new BaseResponse { Success = true });
		}


		[HttpGet()]
		public async Task<ActionResult<List<BotRecord>>> GetCurrentLocations()
		{
			var locations = botService.GetAll();
			logger.LogDebug("extracted {count} locations", locations.Count);
			return await Task.FromResult(locations);
		}
	}
}
