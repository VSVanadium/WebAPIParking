namespace WebAPIParking.DataRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
       
    }
}
