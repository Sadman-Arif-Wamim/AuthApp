namespace AuthProject.Models
{
    public class User
    {
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }

    public class UserDetails
    {
        public string userName { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
    }
}
