using SAAP.CQS.Core.Models;

namespace SAAP.CQS.Core.Services.Commands.UpdateHero
{
    public class UpdateHeroCommandResponse
    {
        #region Public Constructors

        public UpdateHeroCommandResponse(HeroDto hero)
        {
            Hero = hero;
        }

        #endregion

        #region Public Properties

        public HeroDto Hero { get; set; }

        #endregion
    }
}