namespace EBAD_Backend.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }

        public int StatusCode { get; set; }
    }
}
