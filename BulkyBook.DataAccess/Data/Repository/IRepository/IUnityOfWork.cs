using BulkyBook.Models;
using System;

namespace BulkyBook.DataAccess.Data.Repository.IRepository
{
    public interface IUnityOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IShopingCartRepository ShoppingCart { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        ISP_Call SP_Call { get; }

        void Save();
    }
}
