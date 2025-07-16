using Business.Interfaces;
using System.Web.Mvc;

namespace BasitETicaretUygulamasi.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Kategoriye göre ürünleri listele
        public ActionResult ListByCategory(int id)
        {
            var products = _productService.GetByCategoryId(id);
            return View(products);
        }

        // Ürün detay sayfası
        public ActionResult Details(int id)
        {
            var product = _productService.GetById(id);

            if (product == null || product.Stock == 0)
                return HttpNotFound();

            return View(product);
        }
    }
}
