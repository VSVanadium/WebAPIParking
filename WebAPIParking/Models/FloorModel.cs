using System.ComponentModel.DataAnnotations.Schema;
using WebAPIParking.Data;

namespace WebAPIParking.Models
{
    [Table("Floor")]
    public class FloorModel
    {
        public int ID { get; set; }       
        public int TotalCarsSlots { get; set; }
        public int TotalCarsSlotsOccupied { get; set; }
        public int TotalMotorbikeSlots { get; set; }
        public int TotalMotorbikeSlotsOccupied { get; set; }
    }
}
