namespace AuthProject.Models
{
    public class User
    {
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
        public int id { get; set; } = 0;
        public string token { get; set; } = string.Empty;
    }
}
