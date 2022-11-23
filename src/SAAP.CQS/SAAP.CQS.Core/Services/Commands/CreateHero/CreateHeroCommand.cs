using MediatR;

namespace SAAP.CQS.Core.Services.Commands.CreateHero
{
    public sealed class CreateHeroCommand : IRequest<CreateHeroCommandResponse>
    {
        #region Public Constructors

        public CreateHeroCommand(string name)
        {
            Name = name;
        }

        #endregion

        #region Public Properties

        public string Name { get; }

        #endregion
    }
}