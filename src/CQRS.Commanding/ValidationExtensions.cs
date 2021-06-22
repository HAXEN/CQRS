using System.Collections.Generic;

namespace CQRS.Commanding
{
    public static class ValidationExtensions
    {
        public static IEnumerable<IValidationMessage> ToArray(this IValidationMessage validation) =>
            new List<IValidationMessage>() {validation};
    }
}
