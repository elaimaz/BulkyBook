using BulkyBook.Models;

namespace BulkyBook.DataAccess.Data.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType coverType);
    }
}
