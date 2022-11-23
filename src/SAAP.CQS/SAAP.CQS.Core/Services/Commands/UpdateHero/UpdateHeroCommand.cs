using MediatR;

namespace SAAP.CQS.Core.Services.Commands.UpdateHero
{
    public sealed class UpdateHeroCommand : IRequest<UpdateHeroCommandResponse>
    {
        #region Public Properties

        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        #endregion
    }
}