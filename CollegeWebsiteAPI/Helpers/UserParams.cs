namespace CollegeWebsiteAPI.Helpers
{
    public class UserParams : PaginationParams
    {
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string SearchQuery { get; set; }
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public string MobileNumber { get; set; }
    }
}