using Business.Interfaces;
using Business.Services;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Entities;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace BasitETicaretUygulamasi
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
            new Lazy<IUnityContainer>(() =>
            {
                var container = new UnityContainer();
                RegisterTypes(container);
                return container;
            });

        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            // DbContext
            container.RegisterType<AppDbContext, AppDbContext>();

            // Generic Repository
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            container.RegisterType<IGenericRepository<Order>, GenericRepository<Order>>();
            container.RegisterType<IGenericRepository<OrderItem>, GenericRepository<OrderItem>>();


            // Services
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ICategoryService, CategoryService>();

        }

        public static void RegisterComponents()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }
    }
}
