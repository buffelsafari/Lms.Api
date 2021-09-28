using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lms.Core.Entities;

namespace Lms.Data.Data
{
    public class LmsDataContext : DbContext
    {
        public DbSet<Lms.Core.Entities.Course> Course { get; set; }
        public DbSet<Lms.Core.Entities.Module> Module { get; set; }


        public LmsDataContext (DbContextOptions<LmsDataContext> options)
            : base(options)
        {
        }

        
    }
}
