using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.DataAccess.Data;
using mvc.DataAccess.Repository;
using mvc.DataAccess.Repository.IRepository;

namespace MVC23._10._1403.Areas.Admins.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            var objCategoryList = _unitOfWork.CategoryRepo.GetAll();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            //if (category.Name == category.Order.ToString() + "5")
            //{
            //	ModelState.AddModelError("name", "heh!");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Create(category);
                _unitOfWork.Save();

                //_db.SaveChanges();
                TempData["cerMas"] = "cereate shod";
                return RedirectToAction("index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            Category category = _unitOfWork.CategoryRepo.Get(o => o.Id == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category updatedCategory)
        {
            //Category category = _categoryRepo.Get(updatedCategory.Id);
            _unitOfWork.CategoryRepo.Update(updatedCategory);
            _unitOfWork.CategoryRepo.Save();
            //_db._categories.Update(category);
            //_db.SaveChanges();
            TempData["edMas"] = "edit shod";
            return RedirectToAction("index");

        }
        public IActionResult Delete(int id)

        {
            Category category = _unitOfWork.CategoryRepo.Get(o => o.Id == id);

            return View(category);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Category category = _unitOfWork.CategoryRepo.Get(o => o.Id == id);
            _unitOfWork.CategoryRepo.Remove(category);
            _unitOfWork.Save();

            //_db.SaveChanges();
            TempData["delMas"] = "delete shod";
            return RedirectToAction("index");
        }

    }
}
