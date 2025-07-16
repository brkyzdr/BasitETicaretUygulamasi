using Business.Interfaces;
using Business.Services;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Entities;

namespace BasitETicaretUygulamasi.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Context
            container.RegisterType<AppDbContext, AppDbContext>();

            // Generic Repository
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Services
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ICategoryService, CategoryService>();

            // MVC Resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
