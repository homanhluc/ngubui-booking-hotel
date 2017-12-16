using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Ngubui_Project.Models
{
    public class ExclusiveActionAttribute : ActionFilterAttribute
    {
        private static int isExecuting = 0;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Interlocked.CompareExchange(ref isExecuting, 1, 0) == 0)
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            Interlocked.Exchange(ref isExecuting, 0);
        }
    }
}