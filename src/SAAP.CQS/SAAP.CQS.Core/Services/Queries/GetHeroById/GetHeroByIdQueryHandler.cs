using MediatR;
using MediatR.DDD.Exceptions;
using SAAP.CQS.Core.Models;
using SAAP.CQS.Data;

namespace SAAP.CQS.Core.Services.Queries.GetHeroById
{
    public sealed class GetHeroByIdQueryHandler : IRequestHandler<GetHeroByIdQuery, HeroDto>
    {
        #region Private Fields

        private readonly HeroDbContext dbContext;

        #endregion

        #region Public Constructors

        public GetHeroByIdQueryHandler(HeroDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        public async Task<HeroDto> Handle(GetHeroByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Heroes.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException($"Hero with id {request.Id} could not be found.");
            }

            return new HeroDto(entity.Id, entity.Name);
        }

        #endregion
    }
}