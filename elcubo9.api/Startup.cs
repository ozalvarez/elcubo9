using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using elcubo9.api.ActionFilter;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Diagnostics;
using elcubo9.api.Providers;
using System.Web.Routing;

[assembly: OwinStartup(typeof(elcubo9.api.Startup))]

namespace elcubo9.api
{
    public partial class Startup
    {
        public class LoggingPipelineModule : HubPipelineModule
        {
            protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
            {
                Debug.WriteLine("=> Invoking " + context.MethodDescriptor.Name + " on hub " + context.MethodDescriptor.Hub.Name);
                return base.OnBeforeIncoming(context);
            }

            protected override bool OnBeforeOutgoing(IHubOutgoingInvokerContext context)
            {
                Debug.WriteLine("<= Invoking " + context.Invocation.Method + " on client hub " + context.Invocation.Hub);
                return base.OnBeforeOutgoing(context);
            }
        }
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true
                };
                var authorizer = new QueryStringBearerAuthorizeAttribute();
                var module = new AuthorizeModule(authorizer, authorizer);
                GlobalHost.HubPipeline.AddModule(module);
                map.RunSignalR(hubConfiguration);
            });
            GlobalHost.HubPipeline.AddModule(new LoggingPipelineModule());
            ConfigureAuth(app);
        }
    }
}
