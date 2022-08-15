namespace RentACarBlazorServer.Models
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public Boolean? Success { get; set; }
        public string? Message { get; set; }
    }
}
