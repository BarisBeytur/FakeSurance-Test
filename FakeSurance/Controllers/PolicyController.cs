using FakeSurance.DTO;
using FakeSurance.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FakeSurance.ValidationRules;
using FluentValidation.Results;
using Microsoft.Identity.Client;

namespace FakeSurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {

        private readonly Context _context;

        public PolicyController(Context context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("CreatePolicy", Name = "CreatePolicy")]
        public async Task<ActionResult<string>> CreatePolicy([FromHeader]int ProposalId)
        {
            Proposal proposal = await _context.Proposals.Where(i => i.ProposalId == ProposalId).FirstOrDefaultAsync();

            if (proposal == null)
                return BadRequest("Böyle bir teklif bulunamadı!");
            else
            {
                proposal.IsPolicied = true;
                _context.SaveChanges();
            }

            return Ok("Poliçe oluşturuldu!");
        }

    }
}
