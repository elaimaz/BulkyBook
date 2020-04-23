﻿using BulkyBook.DataAccess.Data.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnityOfWork _unitOfWork;

        public CompanyController(IUnityOfWork unityOfWork)
        {
            _unitOfWork = unityOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null)
            {
                //This is for create
                return View(company);
            }
            //This is for edit
            company = _unitOfWork.Company.Get(id.GetValueOrDefault());
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        #region API Call
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Company.GetAll();
            return Json(new { data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Company.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting"});
            }
            _unitOfWork.Company.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Sucefully"});
        }
        
        #endregion
    }
}