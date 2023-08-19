using FakeSurance.DTO.Customer;
using FakeSurance.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeSurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverageController : ControllerBase
    {
        private readonly Context _context;

        public CoverageController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllCoverages", Name = "GetAllCoverages")]
        public async Task<ActionResult<List<Coverage>>> GetAllCoverages()
        {
            var coverages = await _context.Coverages.ToListAsync();
            return Ok(coverages);
        }

        [HttpPost]
        [Route("Create", Name = "CreateCoverage")]
        public async Task<ActionResult<string>> CreateCoverage(Coverage coverage)
        {
            await _context.Coverages.AddAsync(coverage);
            await _context.SaveChangesAsync();
            return Ok("Coverage added!");
        }

        [HttpPut]
        [Route("Update", Name = "UpdateCoverage")]
        public async Task<ActionResult<string>> UpdateCoverage(Coverage coverage)
        {

            var value = await _context.Coverages.FindAsync(coverage.CoverageId);
            value.Name = coverage.Name;
            await _context.SaveChangesAsync();
            return Ok("Coverage updated!");
        }

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteCoverage")]
        public async Task<ActionResult<bool>> DeleteCoverage([FromRoute] int id)
        {
            var matchedcoverage = await _context.Coverages.Where(i => i.CoverageId == id).FirstOrDefaultAsync();

            if (matchedcoverage != null)
            {
                _context.Coverages.Remove(matchedcoverage);
                await _context.SaveChangesAsync();
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }

    }
}
