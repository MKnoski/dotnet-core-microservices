using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Http.Logging
{
    public static class LoggerExtensions
    {
        public static string GetExceptionDetails(this Exception ex)
        {
            var errorString = new StringBuilder();
            errorString.AppendLine("An error occured. ");
            var inner = ex;
            while (inner != null)
            {
                errorString.Append("Error Message:");
                errorString.AppendLine(ex.Message);
                errorString.Append("Stack Trace:");
                errorString.AppendLine(ex.StackTrace);
                inner = inner.InnerException;
            }
            return errorString.ToString();
        }
    }
}