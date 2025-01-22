using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CourseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CourseDto> GetCourseByIdAsync(int courseId)
        {
            Course? course = await _uow.Course.GetCourseByIdAsync(courseId);
            if (course == null) throw new KeyNotFoundException($"Course with ID {courseId} not found.");
            return _mapper.Map<CourseDto>(course);
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _uow.Course.GetAllCoursesAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto dto)
        {
            Course course = _mapper.Map<Course>(dto);
            _uow.Course.Create(course);
            await _uow.CompleteASync();
            return _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> UpdateCourseAsync(int id, JsonPatchDocument<UpdateCourseDto> patchDocument)
        {
            var courseToPatch = await _uow.Course.GetCourseByIdAsync(id, true);
            if (courseToPatch == null) throw new KeyNotFoundException($"{id} not found.");

            var course = _mapper.Map<UpdateCourseDto>(courseToPatch);
            patchDocument.ApplyTo(course);

            _mapper.Map(course, courseToPatch);
            await _uow.CompleteASync();

            return _mapper.Map<CourseDto>(courseToPatch);
        }

        public async Task DeleteCourseAsync(int id)
        {
            var courseToDelete = await _uow.Course.GetCourseByIdAsync(id, true);
            if (courseToDelete == null) throw new KeyNotFoundException($"{id} not found.");
            _uow.Course.Delete(courseToDelete);
            await _uow.CompleteASync();
        }

        public async Task<CourseDto> GetCourseByUserIdAsync(string userId)
        {
            var course = await _uow.Course.GetCourseByUserIdAsync(userId);
            if (course == null) throw new KeyNotFoundException($"Course for user ID {userId} not found.");
            return _mapper.Map<CourseDto>(course);
        }
    }
}
