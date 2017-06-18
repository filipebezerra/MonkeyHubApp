using System.Web.Http.Controllers;
using Microsoft.Azure.Mobile.Server;
using MonkeyHubApp.Backend.Models;
using System.Linq;
using System.Web.Http;
using MonkeyHubApp.Backend.DataObjects;

namespace MonkeyHubApp.Backend.Controllers
{
    public class TagController : TableController<Tag>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MonkeyHubAppContext context = new MonkeyHubAppContext();
            DomainManager = new EntityDomainManager<Tag>(context, Request);
        }

        public IQueryable<Tag> GetAllTags()
        {
            return Query();
        }

        public SingleResult<Tag> GetTag(string id)
        {
            return Lookup(id);
        }
    }
}