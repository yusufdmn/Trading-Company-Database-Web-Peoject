using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Repositories;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureController : ControllerBase
    {
        private readonly FurnitureRepository _furnitureRepository;

        public FurnitureController(FurnitureRepository furnitureRepository)
        {
            _furnitureRepository = furnitureRepository;
        }


        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] InsertDto dto)
        {
            var res = await _furnitureRepository.Insert(dto.Name,dto.BasePrice,dto.TreeMaterial,dto.FurnitureType,dto.Color);
            return Created("furniture created", res);
        }



    }
}
public class InsertDto
{
    public string Name { get; set; }
    public decimal? BasePrice { get; set; }
    public string? TreeMaterial { get; set;}
    public char FurnitureType { get; set; }
    public string Color { get; set; }
}