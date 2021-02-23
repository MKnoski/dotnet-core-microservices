using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastucture.Logging
{
    public static class LoggerExtensions
    {
        public static string GetExceptionDetails(this Exception ex)
        {
            var errorString = new StringBuilder();
            errorString.AppendLine("An error occured. ");
            Exception inner = ex;
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