using APIWeb.Data;
using APIWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ContextoBD _context;

        public SuppliersController(ContextoBD context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> ObtenerProveedores()
        {
            return await _context.Suppliers.OrderBy(s => s.CompanyName).ToListAsync();
        }
    }
}