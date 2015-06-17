using Chetail.API.Data;
using Chetail.Repository;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Chetail.API.App_Start
{
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Load(Assembly.GetExecutingAssembly());

                //Register Services
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }

        }

        // Register Services
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<AppDBContext>().To<AppDBContext>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<IWholesalerRepository>().To<WholesalerRepository>();
            kernel.Bind<IRetailerRepository>().To<RetailerRepository>();
            kernel.Bind<ICartRepository>().To<CartRepository>();
        }        
    }
}