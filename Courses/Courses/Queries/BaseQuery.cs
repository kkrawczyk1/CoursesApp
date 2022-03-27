using System;

namespace Courses.Queries
{
    public class BaseQuery<TResponse> : IQuery<TResponse>
    {
        protected BaseQuery()
        {
            CreateDate = DateTime.Now;
        }

        public DateTime CreateDate { get; set; }
    }
}
