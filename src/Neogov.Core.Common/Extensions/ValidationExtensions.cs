using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Neogov.Core.Common.Exceptions;

namespace Neogov.Core.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static IList<string> ValidateProperties<T>(this T entity) where T: class
        {
            var validationResults = new List<ValidationResult>();
            var vc = new ValidationContext(entity, null, null);
            var isValid = Validator.TryValidateObject(entity, vc, validationResults, true);
            var validator = new List<string>();
            if (!isValid)
                validator.AddRange(validationResults.Select(p => p.ErrorMessage).ToArray());

            return validator;
        }

        public static bool CompareProperties<T>(this T source, T target)
        {
            //TODO - using reflection compare property values
            return true;
        }

        public static void ShouldNotBeNull(this object value, string errorMessage = "Value can't be null")
        {
            value.Ensure(val => val != null, errorMessage);
        }

        public static void Ensure<T>(this T entity, Func<T, bool> predicate, string errorMessage, Action<T> onSuccess = null)
        {
            if (predicate(entity))
            {
                onSuccess?.Invoke(entity);
                return;
            }
            throw new CoreException(errorMessage);
        }
    }
}