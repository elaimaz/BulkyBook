using BulkyBook.Models;

namespace BulkyBook.DataAccess.Data.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company company);
    }
}
