using MediatR;
using MediatR.DDD.Exceptions;
using Microsoft.EntityFrameworkCore;
using SAAP.CQS.Core.Models;
using SAAP.CQS.Data;

namespace SAAP.CQS.Core.Services.Commands.CreateHero
{
    internal sealed class CreateHeroCommandHandler : IRequestHandler<CreateHeroCommand, CreateHeroCommandResponse>
    {
        #region Private Fields

        private readonly HeroDbContext dbContext;

        #endregion

        #region Public Constructors

        public CreateHeroCommandHandler(HeroDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        public async Task<CreateHeroCommandResponse> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
        {
            if (await dbContext.Heroes.AnyAsync(h => h.Name == request.Name, cancellationToken: cancellationToken))
            {
                throw new ConflictException($"Hero with name {request.Name} already exists.");
            }

            var hero = new Hero { Name = request.Name };

            await dbContext.AddAsync(hero, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateHeroCommandResponse(new HeroDto(hero.Id, hero.Name));
        }

        #endregion
    }
}