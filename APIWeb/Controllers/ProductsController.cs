using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIWeb.Data;
using APIWeb.Models;

namespace APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ContextoBD _context;

        public ProductsController(ContextoBD context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest(new { mensaje = "El id de la URL no coincide con el del producto" });
            }

            Normalizar(product);
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product.ProductId = 0;
            Normalizar(product);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { mensaje = "El producto ya no existe" });
            }

            var lineas = await _context.OrderDetails.CountAsync(d => d.ProductId == id);
            if (lineas > 0)
            {
                return Conflict(new
                {
                    mensaje = $"No se puede eliminar «{product.ProductName}»: aparece en {lineas} línea(s) de pedidos. " +
                              "Edítalo y márcalo como descontinuado."
                });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // El <select> manda 0 cuando no se elige nada; en BD eso rompe la llave foránea.
        private static void Normalizar(Product p)
        {
            if (p.SupplierId == 0) p.SupplierId = null;
            if (p.CategoryId == 0) p.CategoryId = null;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
