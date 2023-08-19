using FakeSurance.DTO.Customer;
using FakeSurance.DTO.Proposal;
using FakeSurance.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeSurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly Context _context;

        public CustomerController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllCustomers", Name = "GetAllCustomers")]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
        {

            var customers = _context.Customers.Select(
                i => new CustomerDTO()
                {
                    Id = i.CustomerId,
                    Name = i.Name,
                    Surname = i.Surname,
                    BirthDate = i.BirthDate,
                    IdentityNo = i.IdentityNo,
                    IdentityTypeId = i.IdentityTypeId,
                    IdentityTypeName = ((identityType)i.IdentityTypeId).ToString()
                }
                ).ToList();
            
            return Ok(customers);
        }




        [HttpPost]
        [Route("Create", Name = "CreateCustomer")]
        public async Task<ActionResult<string>> CreateCustomer([FromBody] CustomerDTO customer)
        {

            Customer _customer = new Customer()
            {
                IdentityNo = customer.IdentityNo,
                IdentityTypeId = customer.IdentityTypeId,
                Name = customer.Name,
                Surname = customer.Surname,
                BirthDate = customer.BirthDate,
            };

            _context.Customers.Add(_customer);
            await _context.SaveChangesAsync();
            return Ok("Müşteri eklendi!");
        }



        [HttpPut]
        [Route("Update", Name = "UpdateCustomer")]
        public async Task<ActionResult<string>> UpdateCustomer([FromBody] CustomerDTO customer)
        {
            var matchedcustomer = await _context.Customers.Where(i => i.CustomerId == customer.Id).FirstOrDefaultAsync();
            matchedcustomer.IdentityNo = customer.IdentityNo;
            matchedcustomer.IdentityTypeId = customer.IdentityTypeId;
            matchedcustomer.Name = customer.Name;
            matchedcustomer.Surname = customer.Surname;
            matchedcustomer.BirthDate = customer.BirthDate;
            await _context.SaveChangesAsync();
            return Ok("Güncelleme işlemi yapıldı!");
        }



        [HttpDelete("Delete/{id}", Name = "DeleteCustomerById")]
        public async Task<ActionResult<bool>> DeleteCustomer([FromRoute]int id)
        {

            var matchedcustomer = await _context.Customers.Where(i => i.CustomerId == id).FirstOrDefaultAsync();
            var matchedproposal = await _context.Proposals.Where(i => i.CustomerId == id).FirstOrDefaultAsync();

            if (matchedproposal is null)
            {
                _context.Customers.Remove(matchedcustomer);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Proposals.Remove(matchedproposal);
                _context.Customers.Remove(matchedcustomer);
                await _context.SaveChangesAsync();
            }

            // OK - 200 - Success
            return Ok(true);

        }

    }
}
