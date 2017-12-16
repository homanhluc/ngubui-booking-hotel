using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Ngubui_Project.App_Start.Starup))]

namespace Ngubui_Project.App_Start
{
    public class Starup
    {
        public void Configuration(IAppBuilder app)
        {
            //Liên kết fb
            app.UseFacebookAuthentication(
                appId: "1329561837101010",
                appSecret: "1bc7e9f9225e9503141acd44016efc26");
        }
    }
}
