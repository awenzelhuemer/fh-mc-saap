using FluentValidation;
using FluentValidation.Results;
using MediatR.DDD.Exceptions;
using SAAP.CQS.Core.Services.Contracts;

namespace SAAP.CQS.Core.Services.Validations
{
    public sealed class ValidatorService : IValidatorService
    {
        #region Private Fields

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Public Constructors

        public ValidatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Private Methods

        private IValidator? GetValidator<T>()
        {
            Type validatorType = typeof(IValidator<>).MakeGenericType(typeof(T));
            return _serviceProvider.GetService(validatorType) as IValidator;
        }

        #endregion

        #region Public Methods

        public async Task ValidateAsync<T>(T obj)
        {
            if (obj is null)
            {
                throw new BadRequestException();
            }

            IValidator<T>? validator = (IValidator<T>?)GetValidator<T>();

            if (validator is null)
            {
                throw new NotSupportedException();
            }

            ValidationResult validationResult = await validator.ValidateAsync(obj);

            if (validationResult.IsValid)
            {
                return;
            }

            string errorMessage = string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            throw new BadRequestException(errorMessage);
        }

        #endregion
    }
}