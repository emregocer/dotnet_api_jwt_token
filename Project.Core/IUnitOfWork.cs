using Project.Core.Repository;

namespace Project.Core
{
    public interface IUnitOfWork
    {
        ICardRepository Cards { get; }
        void Complete();
    }
}
