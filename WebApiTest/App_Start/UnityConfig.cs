using System;
using System.Web;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//using Rikrop.Core.Data.Unity;
//using Rikrop.Core.Data.Unity.Repositories;

using WebApiTest.Interfaces;
using WebApiTest.MSSQL;



namespace WebApiTest
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            //container.LoadConfiguration();

            container.RegisterType<IAppDbContext,WebApiTestDB>(new TransientLifetimeManager());
            container.Resolve<IAppDbContext>();
            //container.RegisterRepositoryContext<WebApiTestDB>();
            container.RegisterType<WebApiTestDB>();

            //container.RegisterType<IAuthenticationManager>(
            //    new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            //container.RegisterType<IUserStore<ApplicationUserIdentity>, UserStore<ApplicationUserIdentity>>(
            //    new InjectionConstructor(typeof(ApplicationDbContext)));
        }
    }
}
