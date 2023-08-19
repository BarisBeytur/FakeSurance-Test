namespace FakeSurance.DTO.Vehicle
{
    public class CreateVehicleDTO
    {
        public int CustomerId { get; set; }
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
