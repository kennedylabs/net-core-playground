using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AccountsWebsite.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task PersistChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
