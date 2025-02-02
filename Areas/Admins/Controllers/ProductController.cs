﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvc.DataAccess.Repository.IRepository;
using mvc.Models;
using mvc.Models.ViewModels;

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
			IEnumerable<SelectListItem> categoryListItem = _unitOfWork.CategoryRepo.GetAll()
				.Select(u => new SelectListItem() { Text = u.Name, Value = u.Id.ToString() });
			//ViewBag.CategoryListItem = categoryListItem;
			//ViewData["Title"] = categoryListItem;
			var categoryListItemToView = new ProductVM() { CategoryListItem = categoryListItem, Product = new Product() };
			return View(categoryListItemToView);
		}
		[HttpPost]
		public IActionResult Create(ProductVM obj)
		{
			if (ModelState.IsValid)
			{

				_unitOfWork.ProductRepo.Create(obj.Product);
				_unitOfWork.Save();
				TempData["cerMas"] = "Product creadted sucsessfully";

				return RedirectToAction("Index");
			}

			return View();



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
