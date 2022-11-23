namespace SAAP.CQS.Core.Models
{
    public sealed class HeroDto
    {
        #region Public Constructors

        public HeroDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion

        #region Public Properties

        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        #endregion
    }
}