using FluentValidation;
using MediatR;
using MediatR.DDD.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SAAP.CQS.Core.Services.Commands.CreateHero;
using SAAP.CQS.Core.Services.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SAAP.CQS.FunctionsAPI.Heroes
{
    public class HeroesPost
    {
        #region Private Fields

        private readonly ILogger<HeroesPost> logger;
        private readonly IMediator mediator;

        #endregion

        #region Public Constructors

        public HeroesPost(IMediator mediator, ILogger<HeroesPost> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        #endregion

        #region Public Methods

        [FunctionName("Heroes_Post")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, RouteConfig.POST, Route = RouteConfig.URL)] HttpRequest request)
        {
            try
            {
                string body = await new StreamReader(request.Body).ReadToEndAsync();
                CreateHeroCommand command = JsonConvert.DeserializeObject<CreateHeroCommand>(body);
                CreateHeroCommandResponse response = await mediator.Send(command);

                return new CreatedResult($"{RouteConfig.URL}/{response.Hero.Id}", response.Hero);
            }
            catch (ValidationException ex)
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