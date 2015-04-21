using elcubo9.bll.Exceptions;
using Elmah;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace elcubo9.api.ActionFilter
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class QueryStringBearerAuthorizeAttribute : AuthorizeAttribute
    {
        public override bool AuthorizeHubConnection(Microsoft.AspNet.SignalR.Hubs.HubDescriptor hubDescriptor, IRequest request)
        {
            var _Authorization = request.QueryString.Get("Bearer");
            if (!string.IsNullOrEmpty(_Authorization))
            {
                var ticket = Startup.OAuthOptions.AccessTokenFormat.Unprotect(_Authorization);

                if (ticket != null && ticket.Identity != null && ticket.Identity.IsAuthenticated)
                {
                    request.Environment["server.User"] = new ClaimsPrincipal(ticket.Identity);
                    return true;
                }
            }
            return false;
        }
        public override bool AuthorizeHubMethodInvocation(IHubIncomingInvokerContext hubIncomingInvokerContext, bool appliesToMethod)
        {
            var connectionId = hubIncomingInvokerContext.Hub.Context.ConnectionId;
            var request=hubIncomingInvokerContext.Hub.Context.Request;
            var _Authorization = request.QueryString.Get("Bearer");
            if (!string.IsNullOrEmpty(_Authorization))
            {
                var ticket = Startup.OAuthOptions.AccessTokenFormat.Unprotect(_Authorization);

                if (ticket != null && ticket.Identity != null && ticket.Identity.IsAuthenticated)
                {
                    Dictionary<string, object> _DCI = new Dictionary<string, object>();
                    _DCI.Add("server.User", new ClaimsPrincipal(ticket.Identity));
                    hubIncomingInvokerContext.Hub.Context = new HubCallerContext(new ServerRequest(_DCI), connectionId);
                    return true;
                }
            }
            return false;
        }

    }
}