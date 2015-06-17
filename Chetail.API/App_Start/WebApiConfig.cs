using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Chetail.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // api/Products
            config.Routes.MapHttpRoute(
                name: "ProductsApi",
                routeTemplate: "api/Products/{id}",
                defaults: new { controller = "Products", id = RouteParameter.Optional }
            );

            // api/Products/1/ProductPrices
            config.Routes.MapHttpRoute(
                name: "ProductPricesApi",
                routeTemplate: "api/Products/{productId}/ProductPrices/{id}",
                defaults: new { controller = "ProductPrices", id = RouteParameter.Optional }
            );

            // api/ProductCategories
            config.Routes.MapHttpRoute(
                name: "ProductCategoriesApi",
                routeTemplate: "api/ProductCategories/{id}",
                defaults: new { controller = "ProductCategories", id = RouteParameter.Optional }
            );

            // api/Wholesalers
            config.Routes.MapHttpRoute(
                name: "WholesalersApi",
                routeTemplate: "api/Wholesalers/{id}",
                defaults: new { controller = "Wholesalers", id = RouteParameter.Optional }
            );

            // api/Cart
            config.Routes.MapHttpRoute(
                name: "CartApi",
                routeTemplate: "api/Cart/{id}",
                defaults: new { controller = "Cart", id = RouteParameter.Optional }
            );

            //Set JSON as the default return type for HTML clients such as browsers
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            //Camel Case JSON
            config.Formatters.JsonFormatter.SerializerSettings
                .ContractResolver = new CamelCasePropertyNamesContractResolver();
            //Indent JSON
            config.Formatters.JsonFormatter.SerializerSettings
               .Formatting = Newtonsoft.Json.Formatting.Indented;
        }
    }
}
