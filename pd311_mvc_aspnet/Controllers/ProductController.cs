using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pd311_mvc_aspnet.Models;
using pd311_mvc_aspnet.Repositories.Categories;
using pd311_mvc_aspnet.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace pd311_mvc_aspnet.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model, IFormFile ImageFile)
        {
            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name", model.CategoryId);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                model.Image = "/images/" + uniqueFileName;
            }

            model.Id = Guid.NewGuid().ToString();
            await _productRepository.CreateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _productRepository.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product model, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name", model.CategoryId);
                return View(model);
            }

            var existingProduct = await _productRepository.FindByIdAsync(model.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                existingProduct.Image = "/images/" + uniqueFileName;
            }

            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.Amount = model.Amount;
            existingProduct.CategoryId = model.CategoryId;

            await _productRepository.UpdateAsync(existingProduct);
            return RedirectToAction("Index");
        }
    }
}
