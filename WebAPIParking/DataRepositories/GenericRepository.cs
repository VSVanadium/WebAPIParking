using Microsoft.EntityFrameworkCore;

namespace WebAPIParking.DataRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ConfigDatabaseContext _context;
        private DbSet<T> table = null;

        public GenericRepository(ConfigDatabaseContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

    }
}
