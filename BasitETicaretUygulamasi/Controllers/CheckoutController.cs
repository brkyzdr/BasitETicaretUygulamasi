using BasitETicaretUygulamasi.Helpers;
using System.Web.Mvc;

namespace BasitETicaretUygulamasi.Controllers
{
    public class CheckoutController : Controller
    {
        /// <summary>
        /// Siparişi tamamlama adımı – sahte ödeme yönlendirmesi
        /// </summary>
        public ActionResult Index()
        {
            // Gerçek projede: Sepet verileri işlenir, sipariş veritabanına kaydedilir

            // Bizim projede: Sahte bir "ödeme sayfasına yönlendiriliyorsunuz" ekranı
            CartSessionManager.ClearCart();
            return View();
        }
    }
}
