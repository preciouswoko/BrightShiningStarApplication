namespace SubscriptionApi.Dtos
{
    public class ResponseWrapper<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public T Data { get; set; }
    }
}
