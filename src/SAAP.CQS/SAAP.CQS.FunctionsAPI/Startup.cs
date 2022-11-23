using FluentValidation;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SAAP.CQS.Core.Services.Commands.CreateHero;
using SAAP.CQS.Core.Services.Contracts;
using SAAP.CQS.Core.Services.Validations;
using SAAP.CQS.Data;
using SAAP.CQS.FunctionsAPI;

[assembly: FunctionsStartup(typeof(Startup))]

namespace SAAP.CQS.FunctionsAPI
{
    public sealed class Startup : FunctionsStartup
    {
        #region Public Methods

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMediatR(typeof(CreateHeroCommandValidator));
            builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateHeroCommandValidator));
            builder.Services.AddSingleton<IValidatorService, ValidatorService>();
            builder.Services.AddDbContext<HeroDbContext>();
        }

        #endregion
    }
}