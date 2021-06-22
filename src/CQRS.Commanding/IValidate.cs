using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Commanding
{
    public interface IValidate
    {
        Task<IEnumerable<IValidationMessage>> ValidationMessages(object instance, CancellationToken token);
    }
}