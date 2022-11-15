using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.Helper
{
    public static class ValidationHelper
    {
        internal static StringBuilder GenerateErrorMessage(ValidationResult result)
        {
            StringBuilder sb = new();
            foreach (var error in result.Errors)
            {
                sb.Append(error.ErrorMessage);
                sb.Append(",");
            }

            return sb;
        }
    }
}
