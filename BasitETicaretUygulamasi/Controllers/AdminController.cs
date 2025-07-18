using Business.Interfaces;
using Business.Services;
using Entities;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BasitETicaretUygulamasi.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public AdminController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
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
        public ActionResult AddCategory(Category category, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    // Dosya adı ve yolu
                    var fileName = System.IO.Path.GetFileName(imageFile.FileName);
                    var path = Server.MapPath("~/Content/images/categories/" + fileName);

                    // Fiziksel olarak sunucuya kaydet
                    imageFile.SaveAs(path);

                    // Veritabanı için yol
                    category.ImageUrl = "/Content/images/categories/" + fileName;
                }

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
        public ActionResult EditCategory(Category category, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _categoryService.GetById(category.Id);
                if (existingCategory == null)
                    return HttpNotFound();

                existingCategory.Name = category.Name;

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(imageFile.FileName);
                    var path = Server.MapPath("~/Content/images/categories/" + fileName);
                    imageFile.SaveAs(path);

                    existingCategory.ImageUrl = "/Content/images/categories/" + fileName;
                }

                _categoryService.Update(existingCategory);
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

        public ActionResult ProductList()
        {
            var products = _productService.GetAll();
            return View(products);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var path = Server.MapPath("~/Content/images/products/" + fileName);
                    imageFile.SaveAs(path);

                    product.ImageUrl = "/Content/images/products/" + fileName;
                }

                _productService.Add(product);
                return RedirectToAction("ProductList");
            }

            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return HttpNotFound();

            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                var existing = _productService.GetById(product.Id);
                if (existing == null) return HttpNotFound();

                existing.Name = product.Name;
                existing.Description = product.Description;
                existing.Price = product.Price;
                existing.Stock = product.Stock;
                existing.CategoryId = product.CategoryId;

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var path = Server.MapPath("~/Content/images/products/" + fileName);
                    imageFile.SaveAs(path);

                    existing.ImageUrl = "/Content/images/products/" + fileName;
                }

                _productService.Update(existing);
                return RedirectToAction("ProductList");
            }

            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        public ActionResult DeleteProduct(int id)
        {
            var product = _productService.GetById(id);
            if (product != null)
                _productService.Delete(product);

            return RedirectToAction("ProductList");
        }
    }
}
