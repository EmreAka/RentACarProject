namespace RentACarBlazorServer.Models
{
    public class TokenDto
    {
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
