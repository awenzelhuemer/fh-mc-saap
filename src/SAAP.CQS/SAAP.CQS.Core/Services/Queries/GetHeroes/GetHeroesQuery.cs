using MediatR;
using SAAP.CQS.Core.Models;

namespace SAAP.CQS.Contracts.Queries
{
    public sealed class GetHeroesQuery : IRequest<IEnumerable<HeroDto>>
    {
    }
}