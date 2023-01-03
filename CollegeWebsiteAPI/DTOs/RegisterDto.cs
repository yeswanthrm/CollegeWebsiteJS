using System.ComponentModel.DataAnnotations;
namespace CollegeWebsiteAPI.DTOs
{
    public class RegisterDto
    {
        [Required] 
        public string Name { get; set; }
        [Required] 
        public string Gender { get; set; }
        [Required] 
        public string Email { get; set; }
        [Required] 
        public string MobileNumber { get; set; }
        [Required]
        public IFormFile SSCCertificate { get; set; }
        [Required]
        public IFormFile PUCCertificate { get; set; }
        public IFormFile CasteCertificate { get; set; }
    }
}
