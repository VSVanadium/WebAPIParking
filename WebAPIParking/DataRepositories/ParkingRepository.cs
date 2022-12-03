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
                if (_dbContext.ParkedVehicles.Any(p => p.Id == vehicleId)) return new ParkingResponse(null,0, "The vehicle with Id: "+ vehicleId+" already checked in.", HttpStatusCode.Conflict);

                var avaliableSlot = _dbContext.Slots.FirstOrDefault(p => p.SlotType == vehicleType && !p.IsOccupied);
                if (avaliableSlot != null)
                {
                    ParkingModel newParking = new ParkingModel();
                    newParking.Id = vehicleId;
                    newParking.Type = vehicleType;
                    newParking.Floor = avaliableSlot.FloorId;
                    newParking.Slot = avaliableSlot.Id;
                    newParking.CheckIn = DateTime.UtcNow;

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
                    return new ParkingResponse(newParking,0, "success", HttpStatusCode.OK);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ParkingResponse(null,0, ex.Message, HttpStatusCode.BadRequest);
            }

            return new ParkingResponse(null,0, "Unknow Error", HttpStatusCode.BadRequest);
        }

        public async Task<ParkingResponse> CheckOut(string vehicleId)
        {
            try
            {
                var parkingInfo = _dbContext.ParkedVehicles.Where(p => p.Id == vehicleId).FirstOrDefault();
                if (parkingInfo != null)
                {

                    var price = CalculatePrice(parkingInfo);

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
                    return new ParkingResponse(parkingInfo, price, "The Vehicle with id: " + parkingInfo.Id + " has been sucessfully checked out.", HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new ParkingResponse(null,0, ex.Message, HttpStatusCode.BadRequest);
            }


            return new ParkingResponse(null,0, "Unknow Error", HttpStatusCode.BadRequest);
        }

        private float CalculatePrice(ParkingModel  parking)
        {
            float caluatedPrice = 0.0f;
            DateTime checkIn = parking.CheckIn;
            var minutes = (DateTime.UtcNow - checkIn).TotalMinutes;
            var hours = (DateTime.UtcNow - checkIn).TotalHours;

            if (minutes < 15)
                return 0;

            if (hours < 24)
                caluatedPrice = (float)(hours * 3);
            else
                caluatedPrice = 50.0f;

            return caluatedPrice;
        }
    }
}
