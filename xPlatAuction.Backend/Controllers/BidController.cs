using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using xPlatAuction.Backend.DataObjects;
using xPlatAuction.Backend.Models;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace xPlatAuction.Backend.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class BidController : TableController<Bid>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Bid>(context, Request, Services);
        }

        // GET tables/Bid
        public IQueryable<Bid> GetAllBid()
        {
            return Query(); 
        }

        // GET tables/Bid/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Bid> GetBid(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Bid/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Bid> PatchBid(string id, Delta<Bid> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Bid
        public async Task<IHttpActionResult> PostBid(Bid item)
        {
            ServiceUser user = this.User as ServiceUser;
            
            if(user != null && user.Id != null)
            {
                Services.Log.Info(user.Id);
                item.Bidder = user.Id;
                Bid current = await InsertAsync(item);
                
                //post notification
                var msg = new TemplatePushMessage();
                msg.Add("ItemName", current.AuctionItem.Name);
                msg.Add("BidAmount", item.BidAmount.ToString());
                var outcome = await Services.Push.SendAsync(msg);
                Services.Log.Info(string.Format("Failure: {0}\r\nSuccess: {1}\r\nState: {2}", outcome.Failure, outcome.Success, outcome.State));

                return CreatedAtRoute("Tables", new { id = current.Id }, current);
            }else{
                Services.Log.Info("identity is not present");
                return base.StatusCode(System.Net.HttpStatusCode.BadRequest);
            }
        }

        // DELETE tables/Bid/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteBid(string id)
        {
             return DeleteAsync(id);
        }

    }
}