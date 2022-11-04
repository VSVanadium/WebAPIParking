using Microsoft.AspNetCore.Mvc;
using WebAPIParking.Controllers.Response;
using WebAPIParking.Data;
using WebAPIParking.DataRepositories;
using WebAPIParking.Models;

namespace WebAPIParking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlotController : ControllerBase
    {
        private readonly SlotRepository _slotRepository;
        public SlotController(SlotRepository repo)
        {
            _slotRepository = repo;
        }

        [HttpGet("GetAll")]
        public IEnumerable<SlotModel> Get()
        {
            return _slotRepository.GetAll();
        }

        [HttpPost("Add")]
        public async Task<SlotResponse> Add(int floorId, VehicleType slotType)
        {
            return await _slotRepository.Add(floorId, slotType);
        }

        [HttpPost("Remove")]
        public async Task<SlotResponse> Add(int floorId, int slotId)
        {
            return await _slotRepository.Remove(floorId, slotId);
        }

    }
}
