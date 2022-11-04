using Microsoft.AspNetCore.Mvc;
using WebAPIParking.DataRepositories;
using WebAPIParking.Models;

namespace WebAPIParking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FloorController : ControllerBase
    {
        private readonly FloorRepository _floorRepository;
        public FloorController(FloorRepository repo)
        {
            _floorRepository = repo;
        }

        [HttpGet("GetAll")]
        public IEnumerable<FloorModel> Get()
        {
            return _floorRepository.GetAll();
        }

        [HttpGet("GetAllIds")]
        public IEnumerable<int> GetIds()
        {
            return _floorRepository.GetAll()?.Select(x=>x.ID).ToList();
        }

        [HttpGet("Get")]
        public FloorModel get(int id)
        {
            return _floorRepository.GetById(id);
        }
    }
}
