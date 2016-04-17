using System.Web.Http;
using DotNetBay.WebApp;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace DotNetBay.WebApp {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Use camel case for JSON data
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            app.UseWebApi(config);
        }
    }
}
