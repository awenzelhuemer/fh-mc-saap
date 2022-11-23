using MediatR;
using MediatR.DDD.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SAAP.CQS.Core.Services.Commands.UpdateHero;
using SAAP.CQS.Core.Services.Contracts;
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
        private readonly IValidatorService validatorService;

        #endregion

        #region Public Constructors

        public HeroesPut(IMediator mediator, ILogger<HeroesPost> logger, IValidatorService validatorService)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.validatorService = validatorService;
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

                await validatorService.ValidateAsync(command);
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