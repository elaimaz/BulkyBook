using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using BulkyBook.DataAccess.Data.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BulkyBook.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnityOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnityOfWork unityOfWork)
        {
            _logger = logger;
            _unitOfWork = unityOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var productFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id==id, includeProperties: "Category,CoverType");
            ShoppingCart cartObj = new ShoppingCart() 
            {
                Product = productFromDb,
                ProductId = productFromDb.Id
            };
            return View(cartObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart cartObject)
        {
            cartObject.Id = 0;
            if (ModelState.IsValid)
            {
                //then we will add to cart
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                cartObject.ApplicationUserId = claim.Value;
                ShoppingCart cartFromDb = _unitOfWork.ShopingCart.GetFirstOrDefault(u => u.ApplicationUserId == cartObject.ApplicationUserId && u.ProductId == cartObject.ProductId, includeProperties: "Product");

                if (cartFromDb == null)
                {
                    //no records exists in database for that product for that user
                    _unitOfWork.ShopingCart.Add(cartObject);
                }
                else
                {
                    cartFromDb.Count += cartObject.Count;
                    _unitOfWork.ShopingCart.Update(cartFromDb);
                }
                _unitOfWork.Save();

                var count = _unitOfWork.ShopingCart.GetAll(c => c.ApplicationUserId==cartObject.ApplicationUserId).ToList().Count();

                //HttpContext.Session.SetObject(SD.ssShopingCart, cartObject);
                HttpContext.Session.SetInt32(SD.ssShopingCart, count);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                var productFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == cartObject.ProductId, includeProperties: "Category,CoverType");
                ShoppingCart cartObj = new ShoppingCart() 
                {
                    Product = productFromDb,
                    ProductId = productFromDb.Id
                };
                return View(cartObj);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
