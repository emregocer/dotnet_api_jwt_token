using Project.Core.Model;
using System.Collections.Generic;

namespace Project.Core.Repository
{
    public interface ICardRepository : IRepository<Card>
    {
        IEnumerable<Card> GetBestCards(int count);
    }
}
