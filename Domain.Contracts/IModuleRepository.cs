using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IModuleRepository
    {
        Task<Module?> GetModuleByIdAsync(int moduleId, bool trackChanges = false);
        void Create(Module entity);
        void Update(Module entity);
        void Delete(Module entity);
    }
}
