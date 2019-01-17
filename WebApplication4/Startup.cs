using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.Extensions.DependencyInjection;
using WebApplication4.Models;
using WebApplication4.Repository;

[assembly: OwinStartup(typeof(WebApplication4.Startup))]

namespace WebApplication4
{
    public partial class Startup
    {
       
        public void Configuration(IAppBuilder app)
        {
           
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
        }

        
       
    }
}
