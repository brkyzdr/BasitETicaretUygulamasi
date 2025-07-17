using BasitETicaretUygulamasi.Helpers;
using BasitETicaretUygulamasi.ViewModels;
using Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BasitETicaretUygulamasi.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Sepet sayfası
        /// </summary>
        public ActionResult Index()
        {
            var cart = CartSessionManager.GetCart();
            return View(cart);
        }

        /// <summary>
        /// Ürünü sepete ekler
        /// </summary>
        public ActionResult AddToCart(int id)
        {
            var product = _productService.GetById(id);
            if (product == null || product.Stock <= 0)
                return HttpNotFound();

            var cart = CartSessionManager.GetCart();
            var existingItem = cart.FirstOrDefault(c => c.Product.Id == id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItemViewModel
                {
                    Product = product,
                    Quantity = 1
                });
            }

            CartSessionManager.SetCart(cart);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Ürünü sepetten çıkarır
        /// </summary>
        public ActionResult Remove(int id)
        {
            var cart = CartSessionManager.GetCart();
            var itemToRemove = cart.FirstOrDefault(c => c.Product.Id == id);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                CartSessionManager.SetCart(cart);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Sepeti temizler
        /// </summary>
        public ActionResult Clear()
        {
            CartSessionManager.ClearCart();
            return RedirectToAction("Index");
        }
    }
}
