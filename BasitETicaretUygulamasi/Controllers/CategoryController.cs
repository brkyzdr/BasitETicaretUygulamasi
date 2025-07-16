using Business.Interfaces;
using System.Web.Mvc;

namespace BasitETicaretUygulamasi.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Tüm kategorileri listele (Anasayfa)
        public ActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }
    }
}
