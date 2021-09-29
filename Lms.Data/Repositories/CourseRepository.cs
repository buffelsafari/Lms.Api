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
    public class CourseRepository : ICourseRepository
    {
        private readonly LmsDataContext context;
        public CourseRepository(LmsDataContext context)
        {
            this.context = context;
        }
        public void Add(Course course)
        {
            context.Add(course);            
        }

        public Task<bool> AnyAsync(int? id)
        {
            if (id == null)
            {
                return Task.FromResult(false);
            }
            return context.Course.AnyAsync(c => c.Id == id);           
                        
        }

        public async Task<Course> FindAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await context.Course.FindAsync(id);            
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await context.Course.ToListAsync();

            
        }

        public async Task<Course> GetCourse(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await context.Course.FindAsync(id);
                        
        }

        public void Remove(Course course)
        {
            context.Remove(course);            
        }

        public void Update(Course course)
        {
            context.Update(course);            
        }
    }
}
