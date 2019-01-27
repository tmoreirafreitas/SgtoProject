namespace Sgot.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
