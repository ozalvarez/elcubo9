
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using elcubo9.bll.Exceptions;
using System.Web.Helpers;

namespace elcubo9.api.ActionFilter
{
    public class RuleExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is RuleException)
            {
                var _Ex = (RuleException)context.Exception;
                if (string.IsNullOrEmpty(_Ex.CodeRuleException))
                {
                    context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(_Ex.RuleExceptionMessage),
                        ReasonPhrase = "Rule Exception"
                    };
                }
                else
                {
                    context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(_Ex.ToJson()),
                        ReasonPhrase = "Rule Exception"
                    };
                }
            }
            else base.OnException(context);
        }
    }
}
