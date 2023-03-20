namespace Application.Common.Interfaces
{
    public interface IApplicationDbContextInitialiser
    {
        public void Seed();
        public void TrySeed();
    }
}
