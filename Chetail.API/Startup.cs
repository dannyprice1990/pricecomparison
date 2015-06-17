using Microsoft.Owin;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Web.Cors;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;
using Chetail.API.App_Start;
using Chetail.API.Data.Providers;

// "assembly" attribute specifies which class to fire on startup
[assembly: OwinStartup(typeof(Chetail.API.Startup))]
namespace Chetail.API
{
    public class Startup
    {
        // This class will be fired once our server starts
        public void Configuration(IAppBuilder app)
        {
            // The “app” parameter is an interface used to compose the application for our Owin server.

            //Configure Authentication
            ConfigureOAuth(app);

            // Register API Routes
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            //Usually we would use 'app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);'
            //But we want to override this config so we can add a custom pagination header
            var policy = new CorsPolicy
            {
                AllowAnyHeader=true,
                AllowAnyMethod=true,
                AllowAnyOrigin=true,
                SupportsCredentials = true
            };
            //Custom Header
            policy.ExposedHeaders.Add("X-Pagination");
            app.UseCors(new Microsoft.Owin.Cors.CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = ContextBoundObject => Task.FromResult(policy)
                }
            });


            // Wire up ASP.NET Web API to our Owin server pipeline using Ninject for Dependency Injection
            app.UseNinjectMiddleware(NinjectConfig.CreateKernel)
                .UseNinjectWebApi(config);
 
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}