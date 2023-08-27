using System.Text.Json;

namespace AuthDirectory.Models
{
    public class TokenResponse
    {
        public string username { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
        public bool IncludeAdminClaim { get; set; } = false;
        public Dictionary<string, object> CustomClaims { get; set; } = new Dictionary<string, object>();
    }


}
