using Domain.Models.Entities;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICourseService
    {
        Task<CourseDto> GetCourseByIdAsync(int courseId);
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto);
        Task<CourseDto> UpdateCourseAsync(int id, JsonPatchDocument<UpdateCourseDto> patchDocument);
        Task DeleteCourseAsync(int id);
        Task<CourseDto> GetCourseByUserIdAsync(string userId);
    }
}
