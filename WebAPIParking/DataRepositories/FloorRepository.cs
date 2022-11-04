using Microsoft.EntityFrameworkCore;
using WebAPIParking.Models;

namespace WebAPIParking.DataRepositories
{
    public class FloorRepository
    {
        private readonly ConfigDatabaseContext _dbContext;

        public FloorRepository(ConfigDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<FloorModel> GetAll()
        {
            return _dbContext.Floors;
        }

        public FloorModel GetById(int id)
        {
            return _dbContext.Floors.Where(f => f.ID == id).FirstOrDefault();
        }
    }
}
