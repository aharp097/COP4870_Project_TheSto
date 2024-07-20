using Microsoft.AspNetCore.Mvc;
using Sto.API.EC;
using STO.Library.DTO;
using STO.Models;

namespace Sto.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }
        private readonly ILogger<InventoryController> _logger;
        [HttpGet()]
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return await new InventoryEC().Get();
        }

        [HttpDelete("/{id}")]
        public async Task<ProductDTO?> Delete (int id)
        {
            return await new InventoryEC().Delete(id);
        }

        [HttpPost()]
        public async Task<ProductDTO> AddOrUpdate([FromBody]ProductDTO p)
        {
            return await new InventoryEC().AddOrUpdate(p);
        }
    }
}
