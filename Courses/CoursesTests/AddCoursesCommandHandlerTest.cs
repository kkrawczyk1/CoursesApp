using System;
using System.Threading.Tasks;
using Courses.CommandHandlers;
using Courses.Interfaces;
using Xunit;
using Moq;

namespace CoursesTests
{
    public class AddCoursesCommandHandlerTest
    {
        private readonly Mock<ICourseRepository> _unitOfWork;
        private readonly CreateCourseCommandHandler _handler;
        public AddCoursesCommandHandlerTest()
        {
            _unitOfWork = new Mock<ICourseRepository>();
            _handler = new CreateCourseCommandHandler(_unitOfWork.Object);
        }

        [Fact]
        public async Task HandleCore_NullCommandProvided_ThrowsNullArgumentException()
        {

        }
    }
}