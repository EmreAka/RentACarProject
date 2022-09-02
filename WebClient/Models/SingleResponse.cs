namespace RentACarBlazorServer.Models
{
    public class SingleResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
