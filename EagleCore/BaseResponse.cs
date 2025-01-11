namespace EagleCore
{
	public class BaseResponse
	{
		public static BaseResponse Successful = new() { Success = true };
		public BaseResponse() => Message = string.Empty;
		public string Message { get; set; }
		public bool Success { get; set; }
	}
}
