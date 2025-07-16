using System.Web.Mvc;
using System.Web.Routing;

namespace BasitETicaretUygulamasi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Özel Rota: kategori/{id} → ProductController.ListByCategory
            routes.MapRoute(
                name: "ProductByCategory",
                url: "kategori/{id}",
                defaults: new { controller = "Product", action = "ListByCategory" }
            );

            // Varsayılan Rota
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
