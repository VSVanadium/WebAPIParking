using WebAPIParking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIParking.Models
{
    [Table("Parking")]
    public class ParkingModel
    {
        public string Id { get; set; }
        public VehicleType Type { get; set; }
        public int Floor { get; set; }
        public int Slot { get; set; }

        public ParkingModel()
        {
            Id = "0";
        }
        public ParkingModel(string id, VehicleType type)
        {
            Id = id;
            Type = type;
        }
    }
}
