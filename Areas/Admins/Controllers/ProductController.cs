using Microsoft.AspNetCore.Mvc;
using mvc.DataAccess.Repository.IRepository;
using mvc.Models;

namespace MVC23._10._1403.Areas.Admins.Controllers
{
	public class ProductController : Controller
	{
		private IUnitOfWork _unitOfWork;

		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var Products = _unitOfWork.ProductRepo.GetAll();

			return View(Products);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Product obj)
		{
			//if (ModelState.IsValid)
			
				_unitOfWork.ProductRepo.Create(obj);
				_unitOfWork.Save();
			TempData["cerMas"] = "Product creadted sucsessfully";

				return RedirectToAction("Index");
			
			//return View();

		

		}
		public IActionResult Edit(int id)
		{
			var Products = _unitOfWork.ProductRepo.Get(o => o.Id == id);
			return View(Products);

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
