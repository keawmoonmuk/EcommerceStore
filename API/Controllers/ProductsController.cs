using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //create constucter Storecontext
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        //get all product
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GerProducts()
        {
            var product = await _context.Products.ToListAsync();
            return Ok(product);
        }

        //get id product
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GerProduct(int id)
        {
           return await _context.Products.FindAsync(id);
        }

    }
}