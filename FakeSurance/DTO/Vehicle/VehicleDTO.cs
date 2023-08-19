using FakeSurance.Models;
using System.ComponentModel.DataAnnotations;

namespace FakeSurance.DTO.Vehicle
{
    public class VehicleDTO
    {
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public string OwnerIdentityNo { get; set; }
        public int OwnerIdentityTypeId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public DateTime OwnerBirthDate { get; set; }
        public int PlateCity { get; set; }
        public string PlateDetail { get; set; }
        public string MotorNo { get; set; }
        public string ChassisNo { get; set; }
        public int ManufactureYear { get; set; }
        public string BrandCode { get; set; }
        public string ModelCode { get; set; }
        public DateTime TrafficStartDate { get; set; }
    }
}
