using Business.Interfaces;
using Entities;
using System.Web.Mvc;

namespace BasitETicaretUygulamasi.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;

        public AdminController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Tüm kategorileri listele
        public ActionResult CategoryList()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        // Kategori ekleme (form)
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        // Kategori ekleme (submit)
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Add(category);
                return RedirectToAction("CategoryList");
            }

            return View(category);
        }

        // Kategori düzenleme (form)
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null) return HttpNotFound();
            return View(category);
        }

        // Kategori düzenleme (submit)
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Update(category);
                return RedirectToAction("CategoryList");
            }

            return View(category);
        }

        // Kategori silme
        public ActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (category != null)
            {
                _categoryService.Delete(category);
            }

            return RedirectToAction("CategoryList");
        }
    }
}
