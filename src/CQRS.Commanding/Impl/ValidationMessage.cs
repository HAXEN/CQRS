namespace CQRS.Commanding.Impl
{
    internal class ValidationMessage : IValidationMessage
    {
        public string Property { get; set; }
        public string Message { get; set; }

        internal static IValidationMessage Create(string property, string message)
        {
            return new ValidationMessage
            {
                Property = property,
                Message = message,
            };
        }
    }
}