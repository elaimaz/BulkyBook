using BulkyBook.Models;

namespace BulkyBook.DataAccess.Data.Repository.IRepository
{
    public interface IShopingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart shoppingCart);
    }
}
