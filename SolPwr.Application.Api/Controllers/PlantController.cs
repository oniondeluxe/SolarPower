using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionDlx.SolPwr.Dto;
using OnionDlx.SolPwr.Services;

namespace OnionDlx.SolPwr.Application.Controllers
{
    [Authorize]
    [Route("api")]
    public class PlantController : Controller
    {
        readonly IPlantManagementService _service;

        public PlantController(IPlantManagementService service)
        {
            _service = service;
        }


        [HttpGet]
        [Route("GetAllPlants")]
        public async Task<IEnumerable<PowerPlant>> GetAllPlants()
        {
            var result = await _service.GetAllPlants();
            return result;         
        }


        [HttpPost]
        [Route("CreatePlant")]
        public async Task<IActionResult> CreatePlant([FromBody] PowerPlant dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.CreatePlantAsync(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
