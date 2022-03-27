using Courses.Interfaces;
using Courses.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Courses.Startups
{
    public class AutoFacStartup
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICourseRepository, CourseRepository>();
        }
    }
}
