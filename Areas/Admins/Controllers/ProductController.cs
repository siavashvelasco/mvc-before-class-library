using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvc.DataAccess.Repository.IRepository;
using mvc.Models;
using mvc.Models.ViewModels;
using System.IO;
namespace MVC23._10._1403.Areas.Admins.Controllers
{
	[Area("Admins")]

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
		public IActionResult Upsert(ProductVM obj, IFormFile? file)
		{
			var wwwRoot = _webHostEnvironment.WebRootPath;// فایل سرور
			var filePath = Path.Combine(wwwRoot, @"Image\Product"); //ادرس فایل در فایل سرور
			if (ModelState.IsValid)
			{


				if (file != null)
				{
					if (!string.IsNullOrEmpty(obj.Product.ImageUrl)  )
					{
						var FileAddress = wwwRoot + obj.Product.ImageUrl;

						if (System.IO.File.Exists(FileAddress))
						{
							System.IO.File.Delete(FileAddress);
						}
					}
					using (var theMainFile = new FileStream(Path.Combine(filePath, file.FileName), FileMode.Create))

					{
						file.CopyTo(theMainFile);
					}
					obj.Product.ImageUrl = @"\Image\Product\" + file.FileName;//آدرس url
				}
				if (obj.Product.Id > 0)
				{

					_unitOfWork.ProductRepo.Update(obj.Product);
				}
				else
				{
					_unitOfWork.ProductRepo.Create(obj.Product);


				}
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
