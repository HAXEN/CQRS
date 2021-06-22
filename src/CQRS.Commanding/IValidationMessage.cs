using CQRS.Commanding.Impl;

namespace CQRS.Commanding
{
    public interface IValidationMessage
    {
        public string Property { get; }
        public string Message { get; }

        static IValidationMessage Create(string property, string message) => ValidationMessage.Create(property, message);
    }
}