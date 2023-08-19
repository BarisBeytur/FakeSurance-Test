using FakeSurance.DTO.Product;
using FakeSurance.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeSurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllProducts", Name = "GetAllProducts")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        [Route("Create", Name = "CreateProduct")]
        public async Task<ActionResult<string>> CreateProduct(CreateProductDTO product)
        {

            var addedproduct = new Product()
            {
                Name = product.Name,
                Kod = product.Kod,
                ProductTypeId = product.ProductTypeId,
            };
           
            await _context.Products.AddAsync(addedproduct);
            await _context.SaveChangesAsync();
            return Ok("Product added!");
        }

        [HttpPut]
        [Route("Update", Name = "UpdateProduct")]
        public async Task<ActionResult<string>> UpdateProduct(CreateProductDTO product)
        {

            var matchedproduct = await _context.Products.Where(i => i.ProductId == product.ProductId).FirstOrDefaultAsync();

            matchedproduct.Name = product.Name;
            matchedproduct.Kod = product.Kod;
            matchedproduct.ProductTypeId = product.ProductTypeId;

            await _context.SaveChangesAsync();
            return Ok("Product updated!");
        }

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteProduct")]
        public async Task<ActionResult<bool>> DeleteProduct([FromRoute] int id)
        {
            var matchedproduct = await _context.Products.Where(i => i.ProductId == id).FirstOrDefaultAsync();

            if (matchedproduct != null)
            {
                _context.Products.Remove(matchedproduct);
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
