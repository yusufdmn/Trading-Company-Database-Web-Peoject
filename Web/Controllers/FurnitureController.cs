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
            var res = await _furnitureRepository.Insert(dto.Name, dto.BasePrice, dto.TreeMaterial, dto.FurnitureType, dto.Color);
            return Created("furniture created", res);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteDto dto)
        {
            var res = await _furnitureRepository.Delete(dto.SKU);
            return Created("furniture deleted", res);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateDto dto)
        {
            var res = await _furnitureRepository.Update(dto.SKU, dto.Name, dto.BasePrice, 
                dto.TreeMaterial, dto.FurnitureType, dto.Color);
            return Created("furniture deleted", res);
        }

    }
}


public class DeleteDto
{
    public int SKU { get; set; }
}


public class InsertDto
{
    public string Name { get; set; }
    public decimal? BasePrice { get; set; }
    public string? TreeMaterial { get; set; }
    public char FurnitureType { get; set; }
    public string Color { get; set; }
}

public class UpdateDto
{
    public int SKU { get; set; }
    public string Name { get; set; }
    public decimal? BasePrice { get; set; }
    public string? TreeMaterial { get; set; }
    public char FurnitureType { get; set; }
    public string Color { get; set; }
}