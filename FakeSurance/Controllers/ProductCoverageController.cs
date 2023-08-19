using FakeSurance.DTO.Product;
using FakeSurance.DTO.ProductCoverage;
using FakeSurance.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeSurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCoverageController : ControllerBase
    {

        private readonly Context _context;

        public ProductCoverageController(Context context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("GetAllProductCoverages", Name = "GetAllProductCoverages")]
        public async Task<ActionResult<List<ProductCoverageDTO>>> GetAllProductCoverages()
        {
           

            var products = _context.ProductCoverages.Select(
                i=> new ProductCoverageDTO()
                {
                    productCoverageId = i.ProductCoverageId,
                    productName = i.Product.Name,
                    coverageName = i.Coverage.Name,                   
                    coverageId = i.Coverage.CoverageId,        
                    productId = i.Product.ProductId,
                    productKod = i.Product.Kod,
                    productTypeId = i.Product.ProductTypeId
                }
                ).ToList();

            return Ok(products);
        }

        [HttpPost]
        [Route("Create", Name = "CreateProductCoverage")]
        public async Task<ActionResult<string>> CreateProductCoverage(CreateProductCoverageDTO productcoverage)
        {

            var prodcov = new ProductCoverage()
            {
                CoverageId = productcoverage.CoverageId,
                ProductId = productcoverage.ProductId,
            };

            await _context.ProductCoverages.AddAsync(prodcov);

            await _context.SaveChangesAsync();
            return Ok("ProductCoverage added!");
        }

        [HttpPut]
        [Route("Update", Name = "UpdateProductCoverage")]
        public async Task<ActionResult<string>> UpdateProductCoverage(CreateProductCoverageDTO productcoverage)
        {

            var matchedproduct = await _context.ProductCoverages.Where(i => i.ProductCoverageId == productcoverage.ProductCoverageId).FirstOrDefaultAsync();

            matchedproduct.ProductId = productcoverage.ProductId;
            matchedproduct.CoverageId = productcoverage.CoverageId;

            await _context.SaveChangesAsync();
            return Ok("ProductCoverage updated!");
        }

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteProductCoverage")]
        public async Task<ActionResult<bool>> DeleteProductCoverage([FromRoute] int id)
        {
            var matchedproduct = await _context.ProductCoverages.Where(i => i.ProductCoverageId == id).FirstOrDefaultAsync();

            if (matchedproduct != null)
            {
                _context.ProductCoverages.Remove(matchedproduct);
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
