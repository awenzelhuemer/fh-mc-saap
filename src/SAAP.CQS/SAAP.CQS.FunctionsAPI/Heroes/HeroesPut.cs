using MediatR;
using MediatR.DDD.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SAAP.CQS.Core.Services.Commands.UpdateHero;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SAAP.CQS.FunctionsAPI.Heroes
{
    public class HeroesPut
    {
        #region Private Fields

        private readonly ILogger<HeroesPost> logger;
        private readonly IMediator mediator;

        #endregion

        #region Public Constructors

        public HeroesPut(IMediator mediator, ILogger<HeroesPost> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        #endregion

        #region Public Methods

        [FunctionName("Heroes_Put")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, RouteConfig.PUT, Route = RouteConfig.URL_ID)] HttpRequest request,
            Guid id)
        {
            try
            {
                string body = await new StreamReader(request.Body).ReadToEndAsync();
                UpdateHeroCommand command = JsonConvert.DeserializeObject<UpdateHeroCommand>(body);
                command.Id = id;

                UpdateHeroCommandResponse response = await mediator.Send(command);

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