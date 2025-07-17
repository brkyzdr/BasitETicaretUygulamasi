using BasitETicaretUygulamasi.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace BasitETicaretUygulamasi.Helpers
{
    public class CartSessionManager
    {
        private const string CartSessionKey = "CartItems";

        /// <summary>
        /// Session'dan mevcut sepeti getirir. Yoksa boş liste döner.
        /// </summary>
        public static List<CartItemViewModel> GetCart()
        {
            var cart = HttpContext.Current.Session[CartSessionKey] as List<CartItemViewModel>;

            if (cart == null)
            {
                cart = new List<CartItemViewModel>();
                HttpContext.Current.Session[CartSessionKey] = cart;
            }

            return cart;
        }

        /// <summary>
        /// Sepeti tamamen temizler.
        /// </summary>
        public static void ClearCart()
        {
            HttpContext.Current.Session[CartSessionKey] = null;
        }

        /// <summary>
        /// Session'daki mevcut sepeti günceller.
        /// </summary>
        public static void SetCart(List<CartItemViewModel> cart)
        {
            HttpContext.Current.Session[CartSessionKey] = cart;
        }
    }
}
