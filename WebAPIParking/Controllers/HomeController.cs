using Microsoft.AspNetCore.Mvc;
using WebAPIParking.DataRepositories;
using WebAPIParking.Models;

namespace WebAPIParking.Controllers
{
    public class HomeController : Controller
    {
        private IGenericRepository<ParkingModel> repo = null;

        public HomeController(IGenericRepository<ParkingModel> repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("GetParkings")]
        public ActionResult<IEnumerable<ParkingModel>> GetAll()
        {
            var model = repo.GetAll();
            return Ok(model);
        }

    }
}
