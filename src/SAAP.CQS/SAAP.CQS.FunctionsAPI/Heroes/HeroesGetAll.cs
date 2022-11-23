using MediatR;
using MediatR.DDD.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SAAP.CQS.Contracts.Queries;
using System;
using System.Threading.Tasks;

namespace SAAP.CQS.FunctionsAPI.Heroes
{
    internal class HeroesGetAll
    {
        #region Private Fields

        private readonly ILogger<HeroesGetAll> logger;
        private readonly IMediator mediator;

        #endregion

        #region Public Constructors

        public HeroesGetAll(IMediator mediator, ILogger<HeroesGetAll> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        #endregion

        #region Public Methods

        [FunctionName("Heroes_GetAll")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, RouteConfig.GET, Route = RouteConfig.URL)] HttpRequest request)
        {
            try
            {
                var response = await mediator.Send(new GetHeroesQuery());
                return new OkObjectResult(response);
            }
            catch (BadRequestException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (ConflictException ex)
            {
                return new ConflictObjectResult(ex.Message);
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