using APIWeb.Data;
using APIWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ContextoBD _context;

        public CategoriesController(ContextoBD context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> ObtenerCategorias()
        {
            return await _context.Categories.ToListAsync<Category>();
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Category>>> ObtenerCategoriasSimplificadas()
        //{
        //    return await _context.Categories.ToListAsync<Category>();
        //}
    }
}