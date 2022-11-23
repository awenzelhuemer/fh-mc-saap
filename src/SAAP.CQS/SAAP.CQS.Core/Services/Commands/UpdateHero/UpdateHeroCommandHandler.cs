using MediatR;
using MediatR.DDD.Exceptions;
using SAAP.CQS.Core.Models;
using SAAP.CQS.Data;

namespace SAAP.CQS.Core.Services.Commands.UpdateHero
{
    public sealed class UpdateHeroCommandHandler : IRequestHandler<UpdateHeroCommand, UpdateHeroCommandResponse>
    {
        #region Private Fields

        private readonly HeroDbContext dbContext;

        #endregion

        #region Public Constructors

        public UpdateHeroCommandHandler(HeroDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        public async Task<UpdateHeroCommandResponse> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
        {
            var hero = await dbContext.Heroes.FindAsync(request.Id);

            if (hero is null)
            {
                throw new NotFoundException($"Hero with id {request.Id} does not exist.");
            }

            hero.Name = request.Name;

            await dbContext.SaveChangesAsync();

            return new UpdateHeroCommandResponse(new HeroDto(request.Id, request.Name));
        }

        #endregion
    }
}