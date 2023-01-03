

using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPIParking.Controllers;
using WebAPIParking.Data;
using WebAPIParking.DataRepositories;
using WebAPIParking.Models;

namespace WebAPITest
{
    public class ParkingControllerTest
    {
        private readonly Mock<IGenericRepository<ParkingModel>> service;

        public ParkingControllerTest()
        {
            service = new Mock<IGenericRepository<ParkingModel>>();
        }

        [Fact]
        public void GetParkingList_Test()
        {
            var parkedVehicles = GetSampleParkings();
            service.Setup(x => x.GetAll()).Returns(GetSampleParkings);

            var controller = new HomeController(service.Object);

            var actionResult = controller.GetAll();
            var result = actionResult.Result as OkObjectResult;
            var actual = result?.Value as IEnumerable<ParkingModel>;


            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(parkedVehicles.Count, actual?.Count());
        }

        [Theory(DisplayName = "Get parking for the Vehile")]
        [InlineData("DDF2D")]      
        public void GetParkingByID_Test(string Id)
        {
            var parkedVehicles = GetSampleParkings();
            service.Setup(x => x.GetAll()).Returns(GetSampleParkings);

            var controller = new HomeController(service.Object);


            var actionResult = controller.GetId(Id);
            var result = actionResult.Result as OkObjectResult;
            var actual = result?.Value as ParkingModel;

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(Id, actual?.Id);


        }


        [Theory(DisplayName = "Check-In new Vehicle")]
        [InlineData("DRFC7-X", VehicleType.Car)]
        public void CheckIn_Test(string id, VehicleType vehicleType)
        {
        }


        //Method for check-in, success, failure
        //checkout


        private List<ParkingModel> GetSampleParkings()
        {
            List<ParkingModel> output = new List<ParkingModel>
            {
                new ParkingModel
                {
                    Id= "CB9C4267",
                    Floor = 1,
                    Slot = 1,
                    Type = WebAPIParking.Data.VehicleType.Car,
                    CheckIn = DateTime.UtcNow
                },
                new ParkingModel
                {
                    Id= "4DDA",
                    Floor = 1,
                    Slot = 2,
                    Type = WebAPIParking.Data.VehicleType.Car,
                    CheckIn = DateTime.UtcNow
                },
                new ParkingModel
                {
                    Id= "9230",
                    Floor = 2,
                    Slot = 3,
                    Type = WebAPIParking.Data.VehicleType.Motorbike,
                    CheckIn = DateTime.UtcNow
                },
                new ParkingModel
                {
                    Id= "DDF2D",
                    Floor = 2,
                    Slot = 1,
                    Type = WebAPIParking.Data.VehicleType.Car,
                    CheckIn = DateTime.UtcNow
                },
                new ParkingModel
                {
                    Id= "76198",
                    Floor = 3,
                    Slot = 10,
                    Type = WebAPIParking.Data.VehicleType.Motorbike,
                    CheckIn = DateTime.UtcNow
                }
            };
            return output;
        }


    }
}