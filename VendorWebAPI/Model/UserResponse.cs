namespace VendorWebAPI.Model
{
    public class UserResponse
    {
        public string Token { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public User data { get; set; } = new User();
        
    }
    public class UserListResponse
    {
        public string Token { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<User> data { get; set; } = new List<User>();
    }
}
