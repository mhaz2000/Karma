using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities.Base
{
    public class ExceptionLog : IBaseEntity
    {
        public ExceptionLog() : base()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public static ExceptionLog Create(string requestUrl, string message, string innerExceptionMessage, string stackTrace)
        {
            var newExceptionLog = new ExceptionLog()
            {
                RequestUrl = requestUrl,
                Message = message,
                InnerExceptionMessage = innerExceptionMessage,
                StackTrace = stackTrace,
            };

            return newExceptionLog;
        }

        public string? RequestUrl { get; private set; }
        public string? Message { get; private set; }
        public string? InnerExceptionMessage { get; private set; }
        public string? StackTrace { get; private set; }

        public Guid Id { get; init; }
        public DateTime CreatedAt { get; }
    }
}
