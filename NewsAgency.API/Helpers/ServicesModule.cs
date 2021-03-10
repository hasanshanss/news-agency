using Autofac;
using NewsAgency.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NewsAgency.API.Helpers
{
    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Assembly currentAssembly = Assembly.GetExecutingAssembly();

            Assembly servicesAssembly = Assembly.Load("NewsAgency.Services");
            builder.RegisterAssemblyTypes(servicesAssembly)
                .Where(c => c.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}
