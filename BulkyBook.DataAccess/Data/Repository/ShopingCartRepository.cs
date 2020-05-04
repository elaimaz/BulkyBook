using BulkyBook.DataAccess.Data.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Data.Repository
{
    class ShopingCartRepository : Repository<ShoppingCart>, IShopingCartRepository
    {
        private readonly ApplicationDbContext _db;

        public ShopingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _db.Update(shoppingCart);
        }
    }
}
