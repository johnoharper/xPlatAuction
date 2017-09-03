using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using xPlatAuction.Backend.DataObjects;
using xPlatAuction.Backend.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using xPlatAuction.Backend.Entities;

namespace xPlatAuction.Backend
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();
            options.PushAuthorization = 
                Microsoft.WindowsAzure.Mobile.Service.Security.AuthorizationLevel.User;

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new MobileServiceInitializer());

            AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<AuctionItem, AuctionItemDBEntity>();
                    cfg.CreateMap<AuctionItemDBEntity, AuctionItem>().ForMember(
                        ai => ai.CurrentBid, map => map.UseValue(0.0));
                });
        }
    }

#if DEBUG
    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    {}
#else
    public class MobileServiceInitializer : ClearDatabaseSchemaIfModelChanges<MobileServiceContext>
    { }
#endif

}

