using Project.Core;
using Project.Core.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TestResourceServer.Api.ViewModels;

namespace TestResourceServer.Api.Controllers
{
    [Authorize(Roles = "Mod")]
    [RoutePrefix("api/cards")]
    public class CardController : ApiController
    {
        private readonly IUnitOfWork _uow;

        public CardController(){}

        public CardController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [Route("")]
        public IEnumerable<CardViewModel> GetAll()
        {
            var model = _uow.Cards.GetAll().Select(card => new CardViewModel { Name = card.Name });
            return model;   
        }
    }
}