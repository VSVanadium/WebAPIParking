using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using WebAPIParking.Controllers.Response;
using WebAPIParking.Data;
using WebAPIParking.DataRepositories;
using WebAPIParking.Models;

namespace WebAPIParking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingController : ControllerBase
    {

        private readonly ParkingRepository _parkingRepository;
        public ParkingController(ParkingRepository repo)
        {
            _parkingRepository = repo;
        }

        [HttpGet("GetAll")]
        public IEnumerable<ParkingModel> Get()
        { 
            return  _parkingRepository.GetAll();
        }

        [HttpPost("CheckIn")]
        public async Task<ParkingResponse> CheckIn(string id, VehicleType type)
        {
           return await _parkingRepository.CheckIn(id, type);
        }

        [HttpPost("CheckOut")]
        public async Task<ParkingResponse> CheckOut(string id)
        {
            return await _parkingRepository.CheckOut(id);
            
        }
    }
}