namespace Services.Responses
{
    internal class RestCallResponse<T>
    {
        public T Data { get; set; } = default;

        public ResponseCode ResponseCode { get; set; } = ResponseCode.Success;
    }
}
