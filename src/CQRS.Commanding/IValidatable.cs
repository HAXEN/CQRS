using System.Collections.Generic;

namespace CQRS.Commanding
{
    public interface IValidationMessage
    {
        string Property { get; set; }
        string Message { get; set; }
    }

    public interface IValidatable
    {
        bool IsValid();
        IEnumerable<IValidationMessage> ValidationErrors();
    }
}
