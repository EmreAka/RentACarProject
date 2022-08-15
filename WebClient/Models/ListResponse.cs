namespace RentACarBlazorServer.Models
{
    public class ListResponse<T>
    {
        public List<T>? Data { get; set; }
        public Boolean? Success { get; set; }
        public string? Message { get; set; }
    }
}
