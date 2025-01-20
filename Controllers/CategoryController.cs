using Microsoft.AspNetCore.Mvc;
using MVC23._10._1403.Models;

namespace MVC23._10._1403.Controllers
{
	public class CategoryController : Controller
	{
		private readonly Context _db;
		public CategoryController(Context db)
		{
			_db = db;

		}
		public IActionResult Index()
		{
			var objCategoryList = _db._categories.ToList();
			return View(objCategoryList);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Category category)
		{
			if (category.Name == category.Order.ToString() + "5")
			{
				ModelState.AddModelError("name", "heh!");
			}
			if (ModelState.IsValid)
			{
				_db._categories.Add(category);
				_db.SaveChanges();
				TempData["cerMas"] = "cereate shod";
				return RedirectToAction("index");
			}
			return View();
		}
		public IActionResult Edit(int id)
		{
		Category category = _db._categories.Find(id);
			return View(category);
		}
		[HttpPost]
		public IActionResult Edit(Category updatedCategory) 
		{
			Category category = _db._categories.Find(updatedCategory.Id);
			category.Name = updatedCategory.Name;
			category.Order = updatedCategory.Order;
			_db._categories.Update(category);
			_db.SaveChanges();
			TempData["edMas"] = "edit shod";
			return RedirectToAction("index");

		}
		public IActionResult Delete(int id)

		{
			Category category = _db._categories.Find(id);
			return View(category); 
	
		}
	[HttpPost,ActionName("Delete")]
	public IActionResult DeletePost(int id)
	{
		Category category = _db._categories.Find(id);
		_db._categories.Remove(category);
		_db.SaveChanges();
		TempData["delMas"] = "delete shod";
		return RedirectToAction("index");
	}

}
}
