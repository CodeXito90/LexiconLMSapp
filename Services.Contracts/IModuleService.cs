using Domain.Models.Entities;
using LMS.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IModuleService
    {
        Task<ModuleDto> GetModuleByIdAsync(int moduleId);
        Task<ModuleDto> CreateModuleAsync(CreateModuleDto dto);
    }
}
