using API.Entities;

namespace WebApp.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string Refresh { get; set; }
        public string ID { get; set; }
        public User User { get; set; }
    }
}
