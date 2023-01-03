using System.ComponentModel.DataAnnotations;
namespace CollegeWebsiteAPI.DTOs
{
    public class AdminDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
