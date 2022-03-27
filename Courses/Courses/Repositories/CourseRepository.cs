using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses.Context;
using Courses.Domain.Entity;
using Courses.Interfaces;
using Courses.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddCourse(CourseDTO course)
        {
            var entity = _mapper.Map<Course>(course);
            _context.Courses.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<CourseDTO[]> GetAll()
        {
            var course = await _context.Courses.Include(x=>x.Subjects).ToArrayAsync();

            if (course == null)
            {
                throw new Exception("Dany kurs nie istnieje");
            }

            return _mapper.Map<CourseDTO[]>(course);
        }

        public async Task<CourseDTO> GetById(int courseId)
        {
            var course = await _context.Courses.Include(x => x.Subjects).FirstOrDefaultAsync(x => x.Id.Equals(courseId));

            if (course == null)
            {
                throw new Exception("Dany kurs nie istnieje");
            }

            return _mapper.Map<CourseDTO>(course);
        }

        public async Task DeleteCourse(int courseId)
        {
            var course = await _context.Courses.Include(x=>x.Subjects).FirstOrDefaultAsync(x => x.Id.Equals(courseId));

            if (course == null)
            {
                throw new Exception("Dany kurs nie istnieje");
            }

            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();
        }

        public async Task EditCourse(int courseId, CourseDTO courseDTO)
        {
            var course = await _context.Courses.Include(x=>x.Subjects).FirstOrDefaultAsync(x => x.Id.Equals(courseId));

            if (course == null)
            {
                throw new Exception("Dany kurs nie istnieje");
            }

            course.Name = courseDTO.Name;
            course.Description = courseDTO.Description;
            course.Subjects = new List<Subject>();

            foreach (var subject in courseDTO.Subjects)
            {
                course.Subjects.Add(new Subject()
                {
                    SubjectName = subject.SubjectName,
                    SubjectNumber = subject.SubjectNumber
                });
            }

            await _context.SaveChangesAsync();
        }
    }

}
