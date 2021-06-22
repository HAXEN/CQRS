using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Commanding.Impl
{
    public class DataAnnotationsValidator : IValidate
    {
        public async Task<IEnumerable<IValidationMessage>> ValidationMessages(object instance, CancellationToken token)
        {
            var context = new ValidationContext(instance);
            var validationErrors = new List<ValidationResult>();
            
            if (!Validator.TryValidateObject(instance, context, validationErrors))
            {
                var errors = new List<IValidationMessage>();
                foreach (var result in validationErrors)
                {
                    foreach (var name in result.MemberNames)
                    {
                        errors.Add(IValidationMessage.Create(name, result.ErrorMessage));
                    }
                }

                return errors;
            }

            return new IValidationMessage[0];
        }
    }
}
