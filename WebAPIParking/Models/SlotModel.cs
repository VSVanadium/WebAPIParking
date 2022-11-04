using System.ComponentModel.DataAnnotations.Schema;
using WebAPIParking.Data;

namespace WebAPIParking.Models
{
    [Table("Slot")]
    public class SlotModel
    {
        public int Id { get; set; }
        public VehicleType SlotType { get; set; }
        public int FloorId { get; set; }
        public bool IsOccupied { get; set; }
    }
}
