using MediatR;
using MediatR.DDD.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SAAP.CQS.Core.Services.Queries.GetHeroById;
using System;
using System.Threading.Tasks;

namespace SAAP.CQS.FunctionsAPI.Heroes
{
    internal class HeroesGetOne
    {
        #region Private Fields

        private readonly ILogger<HeroesGetAll> logger;
        private readonly IMediator mediator;

        #endregion

        #region Public Constructors

        public HeroesGetOne(IMediator mediator, ILogger<HeroesGetAll> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        #endregion

        #region Public Methods

        [FunctionName("Heroes_GetOne")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, RouteConfig.GET, Route = RouteConfig.URL_ID)] HttpRequest request,
            Guid id)
        {
            try
            {
                var query = new GetHeroByIdQuery(id);
                var response = await mediator.Send(query);
                return new OkObjectResult(response);
            }
            catch (NotFoundException ex)
            {
                return new NotFoundObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        #endregion
    }
}