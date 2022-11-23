using FluentValidation;

namespace SAAP.CQS.Core.Services.Commands.CreateHero
{
    public sealed class CreateHeroCommandValidator : AbstractValidator<CreateHeroCommand>
    {
        #region Public Constructors

        public CreateHeroCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(5);
        }

        #endregion
    }
}