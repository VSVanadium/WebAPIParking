using Microsoft.AspNetCore.Mvc;
using WebAPIParking.Data;
using WebAPIParking.Models;
using WebAPIParking.Controllers.Response;
using System.Net;

namespace WebAPIParking.DataRepositories
{
    public class SlotRepository
    {
        private readonly ConfigDatabaseContext _dbContext;

        public SlotRepository(ConfigDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<SlotModel> GetAll()
        {
            return _dbContext.Slots;
        }

        public async Task<SlotResponse> Add(int floorId, VehicleType slotType)
        {
            try
            {
                SlotModel slot = new SlotModel();
                slot.FloorId = floorId;
                slot.SlotType = slotType;
                slot.IsOccupied = false;

                _dbContext.Slots.Add(slot);

                var floorTobeUpdated = _dbContext.Floors.Where(f=>f.ID == floorId).FirstOrDefault();
                if (floorTobeUpdated != null)
                {
                    switch(slotType)
                    {
                        case VehicleType.Car: floorTobeUpdated.TotalCarsSlots +=1; break;
                        case VehicleType.Motorbike: floorTobeUpdated.TotalMotorbikeSlots +=1; break;
                    }
                    _dbContext.Update(floorTobeUpdated);
                }

                await _dbContext.SaveChangesAsync();

                return new SlotResponse("New Slot created on the floor:"+floorId+" successfully.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new SlotResponse(ex.Message, HttpStatusCode.BadRequest);
            }

        }


        public async Task<SlotResponse> Remove(int floorId, int slotId)
        {
            try
            {
                var slotTobeRemoved = _dbContext.Slots.Where(s => s.FloorId == floorId && s.Id == slotId).FirstOrDefault();

                if (slotTobeRemoved == null || slotTobeRemoved.IsOccupied)
                    return new SlotResponse("The given slot is occupied", HttpStatusCode.BadRequest);


                _dbContext.Remove(slotTobeRemoved);

                var floorTobeUpdated = _dbContext.Floors.Where(f => f.ID == floorId).FirstOrDefault();
                if (floorTobeUpdated != null)
                {
                    switch (slotTobeRemoved.SlotType)
                    {
                        case VehicleType.Car: floorTobeUpdated.TotalCarsSlots -= 1; break;
                        case VehicleType.Motorbike: floorTobeUpdated.TotalMotorbikeSlots -= 1; break;
                    }
                    _dbContext.Update(floorTobeUpdated);
                }

                await _dbContext.SaveChangesAsync();

                 return new SlotResponse("The given slot is removed", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new SlotResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
