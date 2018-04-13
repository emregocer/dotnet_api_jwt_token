using Project.Core;
using Project.Core.Repository;
using Project.Infrastructure.Repository;

namespace Project.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;

        public UnitOfWork(IDbContext context)
        {
            _context = context;
            Cards = new CardRepository(_context);
        }

        public ICardRepository Cards { get; private set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
