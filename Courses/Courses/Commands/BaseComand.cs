using System;
using Courses.Interfaces;

namespace Courses.Commands
{
    public class BaseCommand : ICommand
    {
        protected BaseCommand()
        {
            CreateDate = DateTime.Now;
        }

        public DateTime CreateDate { get; }
    }
}
