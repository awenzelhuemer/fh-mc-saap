using SAAP.CQS.Core.Models;

namespace SAAP.CQS.Core.Services.Commands.CreateHero
{
    public sealed class CreateHeroCommandResponse
    {
        #region Public Constructors

        public CreateHeroCommandResponse(HeroDto hero)
        {
            Hero = hero;
        }

        #endregion

        #region Public Properties

        public HeroDto Hero { get; }

        #endregion
    }
}