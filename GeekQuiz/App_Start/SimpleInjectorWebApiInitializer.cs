[assembly: WebActivator.PostApplicationStartMethod(typeof(GeekQuiz.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace GeekQuiz.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using GeekQuiz;
    using GeekQuiz.Controllers;
    using System.Net.Http;
    using System.Web;
    using Microsoft.Owin.Security;
    using SimpleInjector.Advanced;
    using System.Collections.Generic;
    using GeekQuiz.Models;

    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            //#error Register your services here (remove this line).

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);

            //container.EnableHttpRequestMessageTracking(GlobalConfiguration.Configuration);
            //container.Register<ApplicationUserManager, ApplicationUserManager>(Lifestyle.Scoped);
            //container.Register<ApplicationSignInManager, ApplicationSignInManager>(Lifestyle.Scoped);

            //container.EnableHttpRequestMessageTracking(GlobalConfiguration.Configuration);
            //HttpContext.GetOwinContext().Get<ApplicationSignInManager>()

            //container.EnableHttpRequestMessageTracking(GlobalConfiguration.Configuration);

            //container.Register<ApplicationSignInManager>(() => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>(), Lifestyle.Scoped);

            //container.RegisterPerWebRequest<ApplicationSignInManager>(() =>
            //            AdvancedExtensions.IsVerifying(container)
            //            ? new OwinContext(new Dictionary<string, object>()).Authentication
            //            : HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>());

            //container.Register<ApplicationSignInManager>(() =>
            //    AdvancedExtensions.IsVerifying(container) ? new OwinContext().Get<ApplicationSignInManager>() : HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>());

            //container.Register<ApplicationUserManager>(() => HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>(), Lifestyle.Scoped);

            //container.Register<ApplicationUserManager>(() =>
            //AdvancedExtensions.IsVerifying(container) ? new OwinContext().Get<ApplicationUserManager>() : HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>());        
        }
    }
}