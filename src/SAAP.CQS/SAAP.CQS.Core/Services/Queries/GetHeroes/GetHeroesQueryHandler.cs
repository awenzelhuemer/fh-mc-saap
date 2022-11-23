using MediatR;
using Microsoft.EntityFrameworkCore;
using SAAP.CQS.Contracts.Queries;
using SAAP.CQS.Core.Models;
using SAAP.CQS.Data;

namespace SAAP.CQS.Core.Services.Queries.GetHeroes
{
    public sealed class GetHeroesQueryHandler : IRequestHandler<GetHeroesQuery, IEnumerable<HeroDto>>
    {
        #region Private Fields

        private readonly HeroDbContext dbContext;

        #endregion

        #region Public Constructors

        public GetHeroesQueryHandler(HeroDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<HeroDto>> Handle(GetHeroesQuery request, CancellationToken cancellationToken)
        {
            var heroes = await dbContext.Heroes.ToListAsync(cancellationToken: cancellationToken);
            return heroes.Select(h => new HeroDto(h.Id, h.Name));
        }

        #endregion
    }
}