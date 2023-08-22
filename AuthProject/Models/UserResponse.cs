namespace AuthProject.Models
{
    public class UserResponse
    {
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
        public int id { get; set; } = 0;
    }
}
