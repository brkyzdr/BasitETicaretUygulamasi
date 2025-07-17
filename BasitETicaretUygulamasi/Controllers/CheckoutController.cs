using System.Web.Mvc;
using Business.Interfaces;
using BasitETicaretUygulamasi.Helpers;

namespace BasitETicaretUygulamasi.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IProductService _productService;

        public CheckoutController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var cart = CartSessionManager.GetCart();

            foreach (var item in cart)
            {
                var product = _productService.GetById(item.Product.Id);
                if (product != null && product.Stock >= item.Quantity)
                {
                    product.Stock -= item.Quantity;
                    _productService.Update(product);
                }
            }

            CartSessionManager.ClearCart();

            return View();
        }
    }
}
