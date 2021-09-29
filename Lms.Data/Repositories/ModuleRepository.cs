using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly LmsDataContext context;
        public ModuleRepository(LmsDataContext context)
        {
            this.context = context;
        }
        public void Add(Module module)
        {
            context.Add(module);            
        }

        public async Task<bool> AnyAsync(int? id)
        {
            if (id == null)
            { 
                return false;
            }
            return await context.Module.AnyAsync(c => c.Id == id);            
        }

        public async Task<Module> FindAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await context.Module.FindAsync(id);            
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await context.Module.ToListAsync();            
        }

        public async Task<Module> GetModule(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await context.Module.FindAsync(id);            
        }

        public void Remove(Module module)
        {
            context.Remove(module);            
        }

        public void Update(Module module)
        {
            context.Update(module);            
        }
    }
}
