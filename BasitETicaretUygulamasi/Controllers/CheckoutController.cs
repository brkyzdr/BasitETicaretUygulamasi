using BasitETicaretUygulamasi.Helpers;
using Business.Interfaces;
using DataAccess.Interfaces;
using Entities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BasitETicaretUygulamasi.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IProductService _productService;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderItem> _orderItemRepository;

        public CheckoutController(IProductService productService,
                                  IGenericRepository<Order> orderRepository,
                                  IGenericRepository<OrderItem> orderItemRepository)
        {
            _productService = productService;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public ActionResult Index()
        {
            var cart = CartSessionManager.GetCart();

            if (!cart.Any())
                return RedirectToAction("Index", "Cart");

            // Siparişi oluştur
            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalPrice = cart.Sum(x => x.Product.Price * x.Quantity),
                OrderItems = new System.Collections.Generic.List<OrderItem>()
            };

            foreach (var item in cart)
            {
                // Ürün güncelle (stok azalt)
                var product = _productService.GetById(item.Product.Id);
                if (product == null || product.Stock < item.Quantity)
                    continue;

                product.Stock -= item.Quantity;
                _productService.Update(product);

                // OrderItem oluştur
                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                order.OrderItems.Add(orderItem);
            }

            // Siparişi kaydet
            _orderRepository.Add(order);
            _orderRepository.Save(); // OrderItems da bu sırada kaydolur (EF cascade save)

            // Sepeti temizle
            CartSessionManager.ClearCart();

            return View();
        }
    }
}
