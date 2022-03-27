using AutoMapper;
using Courses.Domain.Entity;
using Courses.Models;

namespace Courses.Helpers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Subject, SubjectDTO>().ReverseMap();
        }
    }
}
