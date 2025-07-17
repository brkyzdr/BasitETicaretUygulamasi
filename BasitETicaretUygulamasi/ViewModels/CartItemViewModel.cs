using Entities;

namespace BasitETicaretUygulamasi.ViewModels
{
    // Sepette tutulacak ürün + miktar bilgisi
    public class CartItemViewModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
