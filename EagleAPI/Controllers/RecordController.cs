using EagleCore;
using Microsoft.AspNetCore.Mvc;

namespace EagleAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RecordController : ControllerBase
	{
		private readonly ILogger _logger;

		public RecordController(ILogger<RecordController> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// This method receives the records that have been passed from the Drones
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		[HttpPost(Name = "ReceiveLocationRecord")]
		public async Task<ActionResult<BaseResponse>> Post(BotRecord record)
		{
			record.Identifier = Guid.NewGuid(); //Always ignore as we can't trust that the caller has set this up.. Could use Automapper etc and provide a dto, but this is a POC

			_logger.LogInformation("{Lat} {Lon} {Altitude}", record.Location.Latitude, record.Location.Longitude, record.Location.Altitude);
			Console.WriteLine(record.Identifier);
			return await Task.FromResult(new BaseResponse { Success = true });
		}
	}
}
