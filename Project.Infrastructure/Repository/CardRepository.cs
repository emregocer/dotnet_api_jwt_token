using Project.Core;
using Project.Core.Model;
using Project.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(IDbContext context) : base(context)
        {
        }

        public IEnumerable<Card> GetBestCards(int count)
        {
            throw new NotImplementedException();
        }
    }
}
