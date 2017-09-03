using System.Web.Http;
using System.Web.Routing;

namespace xPlatAuction.Backend
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();
        }
    }
}