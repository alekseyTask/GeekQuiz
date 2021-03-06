[assembly: WebActivator.PostApplicationStartMethod(typeof(GeekQuiz.App_Start.SimpleInjectorInitializer), "Initialize")]

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
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            System.Web.Mvc.DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IAuthenticationManager>(() => container.GetInstance<IOwinContext>().Authentication, Lifestyle.Scoped);
            container.Register<IOwinContext>(() =>
                        AdvancedExtensions.IsVerifying(container)
                        ? new OwinContext(new Dictionary<string, object>())
                        : HttpContext.Current.GetOwinContext(), Lifestyle.Scoped);

            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()), Lifestyle.Scoped);

            container.Register<TriviaContext>(Lifestyle.Scoped);
        }
    }
}