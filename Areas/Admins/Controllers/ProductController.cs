using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvc.DataAccess.Repository.IRepository;
using mvc.Models;
using mvc.Models.ViewModels;

namespace MVC23._10._1403.Areas.Admins.Controllers
{
	public class ProductController : Controller
	{
		private IUnitOfWork _unitOfWork;
		private IWebHostEnvironment _webHostEnvironment;
		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			var Products = _unitOfWork.ProductRepo.GetAll();

			return View(Products);
		}
		public IActionResult Upsert(int? id)
		{
			IEnumerable<SelectListItem> categoryListItem = _unitOfWork.CategoryRepo.GetAll()
				.Select(u => new SelectListItem() { Text = u.Name, Value = u.Id.ToString() });
			var vmProduct = new ProductVM() { CategoryListItem = categoryListItem, Product = new Product() };

			if (id == null || id == 0)
			{

				return View(vmProduct);
			}
			else
			{
				var productToEdit = _unitOfWork.ProductRepo.Get(o => o.Id == id);
				vmProduct.Product = productToEdit;
				return View(vmProduct);

			}
		}
		[HttpPost]
		public IActionResult Upsert(ProductVM obj,IFormFile? file)
		{
			var wwwRoot = _webHostEnvironment.WebRootPath;//آدرس سرور
			var filePath = Path.Combine(wwwRoot, @"Image\Product");
			var fileName = /*Guid.NewGuid().ToString()*/ file.FileName;
			using (var theMainFile = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
			{
				file.CopyTo(theMainFile);
			}
			if (ModelState.IsValid)
			{
				obj.Product.ImageUrl = "/Image/Product/" + fileName;//آدرس url
				_unitOfWork.ProductRepo.Create(obj.Product);
				_unitOfWork.Save();
				TempData["cerMas"] = "Product creadted sucsessfully";

				return RedirectToAction("Index");
			}

			return View();



		}

		[HttpPost]
		public IActionResult Edit(Product obj)
		{
			_unitOfWork.ProductRepo.Update(obj);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
		public IActionResult Delete(int id)
		{
			var Products = _unitOfWork.ProductRepo.Get(o => o.Id == id);
			return View(Products);

		}
		[HttpPost]
		public IActionResult Delete(Product obj)
		{
			_unitOfWork.ProductRepo.Remove(obj);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
	}
}
