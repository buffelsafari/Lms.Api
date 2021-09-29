using Lms.Core.Repositories;
using Lms.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public class UoW : IUoW
    {
        private readonly LmsDataContext context;
        private readonly ICourseRepository courseRepository;
        private readonly IModuleRepository moduleRepository;
        public UoW(LmsDataContext context, ICourseRepository courseRepository, IModuleRepository moduleRepository)
        {
            this.context = context;
            this.courseRepository = courseRepository;
            this.moduleRepository = moduleRepository;
        }
        public ICourseRepository CourseRepository => courseRepository;

        public IModuleRepository ModuleRepository => moduleRepository;

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();            
        }
    }
}
