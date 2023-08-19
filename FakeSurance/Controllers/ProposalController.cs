using FakeSurance.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FakeSurance.ValidationRules;
using FluentValidation.Results;
using Microsoft.Identity.Client;
using FakeSurance.DTO.Proposal;
using static System.Net.Mime.MediaTypeNames;

namespace FakeSurance.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ProposalController : ControllerBase
    {
        private readonly Context _context;

        public ProposalController(Context context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("GetOffer", Name = "GetOffer")]
        public async Task<ActionResult<ProposalDTO>> GetOffer([FromBody] ProposalApplicationDTO application)
        {
            var _product = await _context.Products.Where(i => i.Kod == application.kod).FirstOrDefaultAsync();
            var _customer = await _context.Customers.Where(i => i.IdentityNo == application.identityNo).FirstOrDefaultAsync();
            var _vehicle = await _context.Vehicles.Where(i => i.ChassisNo == application.chassisNo).FirstOrDefaultAsync();



            if (_customer is null)
                return BadRequest("Müşteri bulunamadı, lütfen müşteri kaydı oluşturun!");
            else if (_vehicle is null)
                return BadRequest("Girdiğiniz şasi numarasına kayıtlı araç bulunamadı, lütfen araç kaydı oluşturun!");
            else if (_product is null)
                return BadRequest("Girdiğiniz kod numarasıyla eşleşen ürün bulunamadı!");
            else if (_customer.CustomerId != _vehicle.CustomerId)
                return BadRequest("Şasi numarası ile müşteri bilgisi eşleşmedi!");
            else
            {
                Proposal proposal = new Proposal()
                {
                    ProductId = _product.ProductId,
                    NetPremium = 12500,
                    GrossPremium = 10500,
                    InstallmentCount = application.installmentCount,
                    PaymentTypeId = application.paymentTypeId,
                    PaymentMethodId = application.paymentMethodId,
                    CustomerId = _customer.CustomerId,
                    VehicleId = _vehicle.VehicleId,
                    ProposalDate = DateTime.Now,
                    IsPolicied = false
                };

                _context.Proposals.Add(proposal);
                _context.SaveChanges();

                var proposalDTO = new ProposalDTO()
                {
                    ProposalId = proposal.ProposalId,
                    ProductName = _product.Name,
                    NetPremium = proposal.NetPremium,
                    GrossPremium = proposal.GrossPremium,
                    InstallmentCount = proposal.InstallmentCount,
                    PaymentMethodId = proposal.PaymentMethodId,
                    PaymentTypeId = proposal.PaymentTypeId,
                    CustomerName = _customer.Name,
                    CustomerSurname = _customer.Surname,
                    CustomerIdentityNo = _customer.IdentityNo,
                    PlateCity = _vehicle.PlateCity,
                    PlateDetail = _vehicle.PlateDetail,
                    MotorNo = _vehicle.MotorNo,
                    ChassisNo = _vehicle.ChassisNo,
                    ManufactureYear = _vehicle.ManufactureYear,
                    BrandCode = _vehicle.BrandCode,
                    ModelCode = _vehicle.ModelCode,
                    TrafficStartDate = _vehicle.TrafficStartDate,
                    ProposalDate = proposal.ProposalDate,
                    IsPolicied = proposal.IsPolicied
                };

                return Ok(proposalDTO);
            }

        }

        [HttpGet]
        [Route("GetAllProposals", Name = "GetAllProposals")]
        public async Task<ActionResult<List<ProposalDTO>>> GetAllProposals()
        {

            var proposals = _context.Proposals
                .Select(i =>
                    new ProposalDTO()
                    {
                        ProposalId = i.ProposalId,
                        ProductName = i.Product.Name,
                        NetPremium = i.NetPremium,
                        GrossPremium = i.GrossPremium,
                        InstallmentCount = i.InstallmentCount,
                        InstallmentCountName = ((installmentCount)i.InstallmentCount).ToString(),
                        PaymentMethodId = i.PaymentMethodId,
                        PaymentMethodName = ((paymentMethod)i.PaymentMethodId).ToString(),
                        PaymentTypeId = i.PaymentTypeId,
                        PaymentTypeName = ((paymentType)i.PaymentTypeId).ToString(),
                        CustomerName = i.Customer.Name,
                        CustomerSurname = i.Customer.Surname,
                        CustomerIdentityNo = i.Customer.IdentityNo,
                        VehicleId = i.Vehicle.VehicleId,
                        PlateCity = i.Vehicle.PlateCity,
                        PlateDetail = i.Vehicle.PlateDetail,
                        MotorNo = i.Vehicle.MotorNo,
                        ChassisNo = i.Vehicle.ChassisNo,
                        ManufactureYear = i.Vehicle.ManufactureYear,
                        BrandCode = i.Vehicle.BrandCode,
                        ModelCode = i.Vehicle.ModelCode,
                        TrafficStartDate = i.Vehicle.TrafficStartDate,
                        ProposalDate = i.ProposalDate,
                        IsPolicied = i.IsPolicied
                    }
                );

            return Ok(proposals.ToList());
        }




        [HttpPost]
        [Route("CreateProposal", Name = "CreateProposal")]
        public async Task<ActionResult<string>> CreateProposal([FromBody] CreateProposalDTO proposal)
        {
            var _product = await _context.Products.Where(i => i.ProductId == proposal.ProductId).FirstOrDefaultAsync();
            var _customer = await _context.Customers.Where(i => i.CustomerId == proposal.CustomerId).FirstOrDefaultAsync();
            var _vehicle = await _context.Vehicles.Where(i => i.VehicleId == proposal.VehicleId).FirstOrDefaultAsync();

            if (_customer is null)
                return BadRequest("Müşteri bulunamadı, lütfen müşteri kaydı oluşturun!");
            else if (_vehicle is null)
                return BadRequest("Girdiğiniz araç id'sine kayıtlı araç bulunamadı, lütfen araç kaydı oluşturun!");
            else if (_product is null)
                return BadRequest("Girdiğiniz ürün numarasıyla eşleşen ürün bulunamadı!");
            else if (_customer.CustomerId != _vehicle.CustomerId)
                return BadRequest("Araç ile müşteri bilgisi eşleşmedi!");
            else
            {

                Proposal _proposal = new Proposal();
                _proposal.ProductId = proposal.ProductId;
                _proposal.NetPremium = proposal.NetPremium;
                _proposal.GrossPremium = proposal.GrossPremium;
                _proposal.InstallmentCount = proposal.InstallmentCount;
                _proposal.PaymentTypeId = proposal.PaymentTypeId;
                _proposal.PaymentMethodId = proposal.PaymentMethodId;
                _proposal.CustomerId = proposal.CustomerId;
                _proposal.VehicleId = proposal.VehicleId;
                _proposal.ProposalDate = DateTime.Now;
                _proposal.IsPolicied = false;

                _context.Proposals.Add(_proposal);
                _context.SaveChangesAsync();
            }
            return Ok("Teklif oluşturuldu!");

        }



        [HttpPut]
        [Route("UpdateProposal", Name = "UpdateProposal")]
        public async Task<ActionResult<string>> UpdateProposal([FromBody] UpdateProposalDTO proposal)
        {
            if (proposal == null || proposal.ProposalId <= 0)
                return BadRequest("Your value is null or ID is less than 0. ID must be bigger than 0.");

            var existingProposal = await _context.Proposals.Where(i => i.ProposalId == proposal.ProposalId).FirstOrDefaultAsync();

            if (existingProposal == null)
                return NotFound($"The proposal with id {proposal.ProposalId} not found");


            existingProposal.ProposalId = proposal.ProposalId;
            existingProposal.ProductId = proposal.ProductId;
            existingProposal.NetPremium = proposal.NetPremium;
            existingProposal.GrossPremium = proposal.GrossPremium;
            existingProposal.InstallmentCount = proposal.InstallmentCount;
            existingProposal.PaymentTypeId = proposal.PaymentTypeId;
            existingProposal.PaymentMethodId = proposal.PaymentMethodId;
            existingProposal.CustomerId = proposal.CustomerId;
            existingProposal.VehicleId = proposal.VehicleId;
            existingProposal.ProposalDate = proposal.ProposalDate;
            existingProposal.IsPolicied = proposal.IsPolicied;
            existingProposal.PolicyDate = proposal.PolicyDate;
            _context.SaveChangesAsync();

            return Ok("Güncelleme işlemi yapıldı!");

        }



        [HttpDelete]
        [Route("DeleteProposal/{id}", Name = "DeleteProposalById")]
        public async Task<ActionResult<bool>> DeleteProposal(int id)
        {
            // BadRequest - 400 - BadRequest - Client error
            if (id <= 0)
                return BadRequest("ID must be greater than 0!!!");

            var proposal = await _context.Proposals.Where(i => i.ProposalId == id).FirstOrDefaultAsync();
            // NotFound - 404 - NotFound - Client error
            if (proposal == null)
                return NotFound($"The proposal with id {id} not found");

            _context.Proposals.Remove(proposal);
            _context.SaveChangesAsync();

            // OK - 200 - Success
            return Ok(true);

        }

    }


}





