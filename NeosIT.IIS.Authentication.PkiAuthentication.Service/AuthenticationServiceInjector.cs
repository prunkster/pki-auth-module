using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using NeosIT.IIS.Authentication.PkiAuthentication.Module;
using NeosIT.IIS.Authentication.PkiAuthentication.Service;
using Ninject;
using Ninject.Web.Common;
using WebActivator;

[assembly: WebActivator.PreApplicationStartMethod(typeof (AuthenticationServiceInjector), "Start")]
[assembly: ApplicationShutdownMethod(typeof (AuthenticationServiceInjector), "Stop")]

namespace NeosIT.IIS.Authentication.PkiAuthentication.Service
{
    public static class AuthenticationServiceInjector
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IHttpModule>().To<PkiAuthenticationModule>();
            kernel.Bind<IAuthenticationService>().To<PkiAuthenticationService>();
        }
    }
}