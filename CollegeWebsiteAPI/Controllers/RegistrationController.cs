using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollegeWebsiteAPI.Data;
using CollegeWebsiteAPI.DTOs;
using CollegeWebsiteAPI.Entities;
using CollegeWebsiteAPI.Extensions;
using CollegeWebsiteAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public RegistrationController(DataContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewAllRegistrationsDto>>> GetRegisteredStudents([FromQuery] UserParams userParams)
        {
            if(userParams.Username.ToLower() == "kcnadmin" && userParams.Password == "kcnadminblr0!")
            {
                //return await _context.Registrations.ToListAsync();
                var query = _context.Registrations.AsQueryable();
                //if (userParams.Name != null && userParams.Name != String.Empty)
                //{
                //    query = query.Where(u => u.Name.ToLower().Contains(userParams.Name.ToLower()));
                //}
                //if (userParams.Email != null && userParams.Email != String.Empty)
                //{
                //    query = query.Where(u => u.Email.ToLower().Contains(userParams.Email.ToLower()));
                //}
                //if (userParams.MobileNumber != null && userParams.MobileNumber != String.Empty)
                //{
                //    query = query.Where(u => u.MobileNumber.ToLower().Contains(userParams.MobileNumber.ToLower()));
                //}
                if (userParams.SearchQuery != null && userParams.SearchQuery != String.Empty)
                {
                    string searchQuery = userParams.SearchQuery;
                    query = query.Where(u => u.Name.ToLower().Contains(searchQuery.ToLower()) ||
                                             u.Email.ToLower().Contains(searchQuery.ToLower()) ||
                                             u.MobileNumber.ToLower().Contains(searchQuery.ToLower())
                    );
                }

                query = query.OrderByDescending(u => u.Id);
                
                var users = await PagedList<ViewAllRegistrationsDto>.CreateAsync(query.ProjectTo<ViewAllRegistrationsDto>(_mapper
                    .ConfigurationProvider).AsNoTracking(),
                        userParams.PageNumber, userParams.PageSize);
                Response.AddPaginationHeader(users.CurrentPage, users.PageSize,
                users.TotalCount, users.TotalPages);
                return Ok(users);
            }
            else
            {
                return Unauthorized("Invalid admin credentials ");
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromForm] RegisterDto registerDto)
        {
            var registration = _mapper.Map<Registration>(registerDto);


            registration.SSCCertificate = await FileUpload(registerDto.SSCCertificate);

            registration.PUCCertificate = await FileUpload(registerDto.PUCCertificate);
            if (registerDto.CasteCertificate != null)
            {
                registration.CasteCertificate = await FileUpload(registerDto.CasteCertificate);
            }
            if(registration.SSCCertificate == "invalidType" || registration.PUCCertificate == "invalidType" ||
                registration.CasteCertificate == "invalidType")
            {
                return BadRequest("only JGP, PNG or PDF documents are allowed");
            }
            if (registration.SSCCertificate == "tooBig" || registration.PUCCertificate == "tooBig" ||
                registration.CasteCertificate == "tooBig")
            {
                return BadRequest("File size can'r exceed 200KB");
            }
            _context.Registrations.Add(registration);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Ok("Registration Sucessful");
            }
            else
            {
                return BadRequest("Error occured during registration");
            }
            
        }
        private async Task<string> FileUpload(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (extension.ToLower() != ".png" && extension.ToLower() != ".jpg" && extension.ToLower() != ".jpeg"
                && extension.ToLower() != ".pdf")
            {
                return "invalidType";
            }
            else if (file.Length > 200000)
            {
                return "tooBig";
            }
            Guid id = Guid.NewGuid();
            
            
            string uniqueFileName = id.ToString() + extension;
            var filePath = Path.Combine(_environment.ContentRootPath, "Images", uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return uniqueFileName;

        }
    }
}
