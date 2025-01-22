using LMS.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IActivityService
    {
        Task<ActivityDto> GetActivityAsync(int activityId);
    }
}
