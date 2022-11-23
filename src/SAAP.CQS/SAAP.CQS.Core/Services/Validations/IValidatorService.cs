namespace SAAP.CQS.Core.Services.Contracts
{
    public interface IValidatorService
    {
        #region Public Methods

        Task ValidateAsync<T>(T obj);

        #endregion
    }
}