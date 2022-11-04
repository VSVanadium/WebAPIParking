using Microsoft.AspNetCore.Mvc;
using WebAPIParking.Controllers.Response;
using WebAPIParking.Data;
using WebAPIParking.Models;
using System.Net;

namespace WebAPIParking.DataRepositories
{
    public class ParkingRepository
    {
        private readonly ConfigDatabaseContext _dbContext;
        public ParkingRepository(ConfigDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<ParkingModel> GetAll()
        {
            return _dbContext.ParkedVehicles;
        }

        public async Task<ParkingResponse> CheckIn(string vehicleId, VehicleType vehicleType)
        {
            try
            {
                if (_dbContext.ParkedVehicles.Any(p => p.Id == vehicleId)) return new ParkingResponse(null, "The vehicle with Id: "+ vehicleId+" already checked in.", HttpStatusCode.Conflict);

                var avaliableSlot = _dbContext.Slots.FirstOrDefault(p => p.SlotType == vehicleType && !p.IsOccupied);
                if (avaliableSlot != null)
                {
                    ParkingModel newParking = new ParkingModel();
                    newParking.Id = vehicleId;
                    newParking.Type = vehicleType;
                    newParking.Floor = avaliableSlot.FloorId;
                    newParking.Slot = avaliableSlot.Id;

                    _dbContext.Add(newParking);
                   

                    avaliableSlot.IsOccupied = true;
                    _dbContext.Update(avaliableSlot);
                    
                    var FloorToUpdate = _dbContext.Floors.Where(f => f.ID == avaliableSlot.FloorId).FirstOrDefault();
                    if (FloorToUpdate != null)
                    {
                        switch(vehicleType)
                        {
                            case VehicleType.Car:  FloorToUpdate.TotalCarsSlotsOccupied += 1; break;
                            case VehicleType.Motorbike: FloorToUpdate.TotalMotorbikeSlotsOccupied += 1; break;
                        }

                        _dbContext.Update(FloorToUpdate);
                        
                    }

                    await _dbContext.SaveChangesAsync();
                    return new ParkingResponse(newParking, "success", HttpStatusCode.OK);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ParkingResponse(null, ex.Message, HttpStatusCode.BadRequest);
            }

            return new ParkingResponse(null, "Unknow Error", HttpStatusCode.BadRequest);
        }

        public async Task<ParkingResponse> CheckOut(string vehicleId)
        {
            try
            {
                var parkingInfo = _dbContext.ParkedVehicles.Where(p => p.Id == vehicleId).FirstOrDefault();
                if (parkingInfo != null)
                {
                    _dbContext.Remove(parkingInfo);

                    var slotToBeUpadated = _dbContext.Slots.FirstOrDefault(s => s.Id == parkingInfo.Slot);
                    if (slotToBeUpadated != null)
                    {
                        slotToBeUpadated.IsOccupied = false;
                        _dbContext.Update(slotToBeUpadated);

                    }

                    var FloorToUpdate = _dbContext.Floors.Where(f => f.ID == parkingInfo.Floor).FirstOrDefault();
                    if (FloorToUpdate != null)
                    {
                        switch (parkingInfo.Type)
                        {
                            case VehicleType.Car: FloorToUpdate.TotalCarsSlotsOccupied -= 1; break;
                            case VehicleType.Motorbike: FloorToUpdate.TotalMotorbikeSlotsOccupied -= 1; break;
                        }

                        _dbContext.Update(FloorToUpdate);

                    }


                    await _dbContext.SaveChangesAsync();
                    return new ParkingResponse(parkingInfo, "The Vehicle with id: " + parkingInfo.Id + " has been sucessfully checked out.", HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new ParkingResponse(null, ex.Message, HttpStatusCode.BadRequest);
            }


            return new ParkingResponse(null, "Unknow Error", HttpStatusCode.BadRequest);
        }
    }
}
