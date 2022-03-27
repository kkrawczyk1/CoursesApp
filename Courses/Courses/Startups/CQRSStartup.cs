using Courses.CommandHandlers;
using Courses.Commands;
using Courses.Interfaces;
using Courses.Models;
using Courses.Queries;
using Courses.QueryHandlers;
using Infrastructure.Bus.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Courses.Startups
{
    public class CQRSStartup
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();

            services.AddMediatR(typeof(CommandBus));
            services.AddMediatR(typeof(QueryBus));

            // Command Handlers
            services.AddTransient<IRequestHandler<CreateCourseCommand, Unit>, CreateCourseCommandHandler>();
            services.AddTransient<IRequestHandler<EditCourseCommand, Unit>, EditCourseCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteCourseCommand, Unit>, DeleteCourseCommandHandler>();

            // Query Handlers
            services.AddTransient<IRequestHandler<GetCourseByIdQuery, CourseDTO>, GetCourseByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetCoursesQuery, CourseDTO[]>, GetCoursesQueryHandler>();
        }
    }
}
