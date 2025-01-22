using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs
{
    // Course DTOs
    public abstract class CourseBaseDto
    {
        [Required]
        [MinLength(1)]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }

    public class CourseDto : CourseBaseDto
    {
        public int CourseId { get; set; }
        [Display(Name = "CourseName")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "StartDate")]
        public DateTime StartDate { get; set; }
        [Display(Name = "EndDate")]
        public DateTime EndDate { get; set; }
        [Display(Name = "CourseDocuments")]
        public List<CourseFileDto> CourseFilesDto { get; set; }
        [Display(Name = "Modules")]
        public List<ModuleDto> Modules { get; set; }
        [Display(Name = "EnrolledUsers")]
        public List<UserDto> Users { get; set; }

    }

    public class CourseFileDto
    {
        public int CourseFIleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UploadedAt { get; set; }
    }

    public class CreateCourseDto : CourseBaseDto
    {
        public List<ModuleDto>? Modules { get; set; }
    }

    public class UpdateCourseDto : CourseBaseDto
    {
        public int CourseId { get; set; }
        public List<UserDto>? Users { get; set; }
        public List<ModuleDto>? Modules { get; set; }
        public List<DocumentDto>? Documents { get; set; }
    }

}
