using BulkyBook.Models;
using System.Linq;

namespace BulkyBook.DataAccess.Data.Repository.IRepository
{
    class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
