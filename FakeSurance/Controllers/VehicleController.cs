using FakeSurance.DTO.Proposal;
using FakeSurance.DTO.Vehicle;
using FakeSurance.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;

namespace FakeSurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly Context _context;

        public VehicleController(Context context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("GetAllVehicles", Name = "GetAllVehicles")]
        public async Task<ActionResult<List<VehicleDTO>>> GetAllVehicles()
        {
            var vehicles = _context.Vehicles.Select(i =>
            new VehicleDTO()
            {
                VehicleId = i.VehicleId,
                CustomerId = i.Customer.CustomerId,
                OwnerIdentityNo = i.Customer.IdentityNo,
                OwnerIdentityTypeId = i.Customer.IdentityTypeId,
                OwnerName = i.Customer.Name,
                OwnerSurname = i.Customer.Surname,
                OwnerBirthDate = i.Customer.BirthDate,
                PlateCity = i.PlateCity,
                PlateDetail = i.PlateDetail,
                MotorNo = i.MotorNo,
                ChassisNo = i.ChassisNo,
                ManufactureYear = i.ManufactureYear,
                BrandCode = i.BrandCode,
                ModelCode = i.ModelCode,
                TrafficStartDate = i.TrafficStartDate,
            });

            return Ok(vehicles.ToList());
        }




        [HttpPost]
        [Route("Create", Name = "CreateVehicle")]
        public async Task<ActionResult<string>> CreateVehicle([FromBody] CreateVehicleDTO vehicle)
        {

            Vehicle _vehicle = new Vehicle()
            {
                CustomerId = vehicle.CustomerId,
                PlateCity = vehicle.PlateCity,
                PlateDetail = vehicle.PlateDetail,
                MotorNo = vehicle.MotorNo,
                ChassisNo = vehicle.ChassisNo,
                ManufactureYear = vehicle.ManufactureYear,
                BrandCode = vehicle.BrandCode,
                ModelCode = vehicle.ModelCode,
                TrafficStartDate = vehicle.TrafficStartDate,
            };

            _context.Vehicles.Add(_vehicle);
            await _context.SaveChangesAsync();
            return Ok("Araç eklendi!");

        }



        [HttpPut]
        [Route("Update", Name = "UpdateVehicle")]
        public async Task<ActionResult<string>> UpdateVehicle([FromBody] UpdateVehicleDTO vehicle)
        {

            var matchedvehicle = await _context.Vehicles.Where(i => i.VehicleId == vehicle.VehicleId).FirstOrDefaultAsync();

            matchedvehicle.VehicleId = vehicle.VehicleId;
            matchedvehicle.CustomerId = vehicle.CustomerId;
            matchedvehicle.PlateCity = vehicle.PlateCity;
            matchedvehicle.PlateDetail = vehicle.PlateDetail;
            matchedvehicle.MotorNo = vehicle.MotorNo;
            matchedvehicle.ChassisNo = vehicle.ChassisNo;
            matchedvehicle.ManufactureYear = vehicle.ManufactureYear;
            matchedvehicle.BrandCode = vehicle.BrandCode;
            matchedvehicle.ModelCode = vehicle.ModelCode;
            matchedvehicle.TrafficStartDate = vehicle.TrafficStartDate;

            await _context.SaveChangesAsync();

            return Ok("Güncelleme işlemi yapıldı!");
        }



        [HttpDelete("Delete/{id}", Name = "DeleteVehicleById")]
        public async Task<ActionResult<bool>> DeleteVehicle([FromRoute] int id)
        {

            var matchedvehicle = await _context.Vehicles.Where(i => i.VehicleId == id).FirstOrDefaultAsync();
            var matchedproposal = await _context.Proposals.Where(i => i.VehicleId == id).FirstOrDefaultAsync();

            if (matchedproposal is null)
            {
                _context.Vehicles.Remove(matchedvehicle);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Proposals.Remove(matchedproposal);
                _context.Vehicles.Remove(matchedvehicle);
                await _context.SaveChangesAsync();
            }

            // OK - 200 - Success
            return Ok(true);

        }
    }
}

