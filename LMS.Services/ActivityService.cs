using AutoMapper;
using LMS.Infrastructure.Data;
using LMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Contracts;
using Domain.Contracts;

namespace LMS.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ActivityService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ActivityDto> GetActivityAsync(int activityId)
        {
            Activity? activity = await _uow.Activity.GetActivityByIdAsync(activityId);
            return _mapper.Map<ActivityDto>(activity);
        }
        public async Task<ActivityDto> CreateActivityAsync(ActivityDto activityDto)
        {
            var activity = _mapper.Map<Activity>(activityDto);
            _uow.Activity.Create(activity);
            await _uow.CompleteASync();
            return _mapper.Map<ActivityDto>(activity);
        }


        public async Task<bool> DeleteActivityAsync(int activityId)
        {
            var activity = await _uow.Activity.GetActivityByIdAsync(activityId);
            if (activity == null)
            {
                return false; // Activity not found
            }

            _uow.Activity.Delete(activity);
            await _uow.CompleteASync();
            return true;
        }

        //public async Task<IEnumerable<ActivityDto>> GetActivitiesByModuleIdAsync(int moduleId)
        //{
        //    var activities = await _uow.Activity.GetActivitiesByModuleIdAsync(moduleId);
        //    return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        //}


        public async Task<bool> UpdateActivityAsync(int activityId, ActivityDto activityDto)
        {
            var activity = await _uow.Activity.GetActivityByIdAsync(activityId);
            if (activity == null)
            {
                return false; // Activity not found
            }

            _mapper.Map(activityDto, activity); // Update the existing entity with new values
            _uow.Activity.Update(activity);
            await _uow.CompleteASync();
            return true;
        }
    }
}
