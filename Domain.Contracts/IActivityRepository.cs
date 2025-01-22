using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IActivityRepository
    {
        Task<Activity?> GetActivityByIdAsync(int activityId, bool trackChanges = false);
        Task GetActivitiesByModuleIdAsync(int moduleId);
        void Create(Activity entity);
        void Update(Activity entity);
        void Delete(Activity entity);
    }
}
