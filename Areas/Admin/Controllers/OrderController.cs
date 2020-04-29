using BulkyBook.DataAccess.Data.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnityOfWork _unitOfWork;

        public OrderController(IUnityOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetOrderList()
        {
            IEnumerable<OrderHeader> orderHeaderList;

            orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties:"ApplicationUser");

            return Json(new { data = orderHeaderList});
        }

        #endregion
    }
}