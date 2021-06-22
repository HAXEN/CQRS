using CQRS.Commanding.Impl;

namespace CQRS.Commanding
{
    public class Configuration
    {
        private IValidate _validator = new DataAnnotationsValidator();

        internal IValidate Validator => _validator;

        public Configuration SetValidator(IValidate validator)
        {
            _validator = validator;
            return this;
        }
    }
}
