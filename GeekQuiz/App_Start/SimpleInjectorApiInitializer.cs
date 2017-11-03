namespace GeekQuiz.App_Start
{
    using GeekQuiz.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using SimpleInjector;
    using SimpleInjector.Advanced;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using SimpleInjector.Integration.WebApi;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;

    public static class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            InitializeWebAPI();
            InitializeMvc();
        }

        public static void InitializeWebAPI()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            InitializeContainerWebApi(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static void InitializeMvc()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainerMvc(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainerMvc(Container container)
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
        }

        private static void InitializeContainerWebApi(Container container)
        {
            container.Register<TriviaContext>(Lifestyle.Scoped);
        }
    }
}