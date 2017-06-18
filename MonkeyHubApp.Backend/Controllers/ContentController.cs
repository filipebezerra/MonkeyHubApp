using System.Web.Http.Controllers;
using Microsoft.Azure.Mobile.Server;
using MonkeyHubApp.Backend.Models;
using System.Linq;
using System.Web.Http;
using MonkeyHubApp.Backend.DataObjects;
using MonkeyHubApp.Backend.Helpers;

namespace MonkeyHubApp.Backend.Controllers
{
    public class ContentController : TableController<Content>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MonkeyHubAppContext context = new MonkeyHubAppContext();
            DomainManager = new EntityDomainManager<Content>(context, Request);
        }

        [ExpandProperty("Tag")]
        public IQueryable<Content> GetAllTags()
        {
            return Query();
        }

        [ExpandProperty("Tag")]
        public SingleResult<Content> GetTag(string id)
        {
            return Lookup(id);
        }
    }
}