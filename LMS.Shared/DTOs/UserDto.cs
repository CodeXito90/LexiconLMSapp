using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs
{
    public record UserForRegistrationDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string LastName { get; set; }
    }
    public class UserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public CourseDto Course { get; set; }

        public string Role { get; set; }
    }

    public class UpdateUserDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
