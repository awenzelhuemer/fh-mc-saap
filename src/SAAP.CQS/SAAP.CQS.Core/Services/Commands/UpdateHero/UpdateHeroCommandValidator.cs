using FluentValidation;

namespace SAAP.CQS.Core.Services.Commands.UpdateHero
{
    public sealed class UpdateHeroCommandValidator : AbstractValidator<UpdateHeroCommand>
    {
        #region Public Constructors

        public UpdateHeroCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(5);
        }

        #endregion
    }
}