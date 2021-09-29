using Bogus;
using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data
{
    public class SeedData
    {

        public static async Task InitAsync(LmsDataContext context, IServiceProvider services)
        {
            Faker faker = new Faker("sv");

            List<Course> courses = new List<Course>();

            for (int i = 0; i < 20; i++)
            {
                List<Module> modules = new List<Module>();
                for (int j = 0; j < faker.Random.Int(1, 5); j++)
                {
                    modules.Add(new Module
                    {
                        Title = faker.Commerce.ProductAdjective(),
                        StartDate = DateTime.Now.AddDays(faker.Random.Int(-3, 3))

                    });
                    
                }

                courses.Add(new Course
                {
                    Title = faker.Commerce.ProductName(),
                    StartDate = DateTime.Now.AddDays(faker.Random.Int(-3, 3)),
                    Modules = modules
                    
                });
                

            }

            context.AddRange(courses);
            await context.SaveChangesAsync();

        }


    }
}
