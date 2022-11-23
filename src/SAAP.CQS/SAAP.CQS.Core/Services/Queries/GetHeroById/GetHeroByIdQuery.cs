using MediatR;
using SAAP.CQS.Core.Models;

namespace SAAP.CQS.Core.Services.Queries.GetHeroById
{
    public sealed class GetHeroByIdQuery : IRequest<HeroDto>
    {
        #region Public Constructors

        public GetHeroByIdQuery(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Public Properties

        public Guid Id { get; set; }

        #endregion
    }
}