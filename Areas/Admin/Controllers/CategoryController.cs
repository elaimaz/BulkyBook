using BulkyBook.DataAccess.Data.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnityOfWork _unitOfWork;

        public CategoryController(IUnityOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int productPage = 1)
        {
            CategoryVM categoryVM = new CategoryVM()
            {
                Categories = await _unitOfWork.Category.GetAllAsync()
            };
            var count = categoryVM.Categories.Count();
            categoryVM.Categories = categoryVM.Categories.OrderBy(p => p.Name).Skip((productPage - 1) * 2).Take(2).ToList();

            categoryVM.PaginInfo = new PaginInfo 
            {
                CurrentPage = productPage,
                ItemPerPage = 2,
                TotalItem = count,
                UrlParam = "/Admin/Category/Index?productPage=:"
            };
            return View(categoryVM);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                //This is for create
                return View(category);
            }
            //this is for edit
            category = await _unitOfWork.Category.GetAsync(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    await _unitOfWork.Category.AddAsync(category);
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allObj = await _unitOfWork.Category.GetAllAsync();
            return Json(new { data = allObj});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = await _unitOfWork.Category.GetAsync(id);
            if (objFromDb == null)
            {
                TempData["Error"] = "Error Deleting Category";
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Category Succefully Deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}