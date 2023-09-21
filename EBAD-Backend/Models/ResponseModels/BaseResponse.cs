namespace EBAD_Backend.Models.ResponseModels
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {

        }

        public BaseResponse(string message, T data)
        {
            Message = message;
            Data = data;
        }

        public BaseResponse(string message)
        {
            Message = message;
            Success = false;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
