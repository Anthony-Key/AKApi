using AKAPI.Models;
using AKAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AKAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        private INationalParkRepository _npRepository;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository npRepo, IMapper mapper)
        {
            _npRepository = npRepo;
            _mapper = mapper;
        }

        [Authorize(Roles ="test")]
        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var parks = _npRepository.GetNationalParks();
            var parkDTO = new List<NationalParkDTO>();

            foreach(var park in parks)
            {
                parkDTO.Add(_mapper.Map<NationalParkDTO>(park));
            }

            return Ok(parkDTO);
        }

        [Authorize(Roles = "test")]
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var park = _npRepository.GetNationalPark(nationalParkId);

            if (park == null)
            {
                return NotFound();
            }

            var parkDTO = _mapper.Map<NationalParkDTO>(park);

            return Ok(parkDTO);
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDTO nationalPark)
        {
            if (nationalPark == null)
            {
                return BadRequest(ModelState);
            }

            if (_npRepository.NationalParkExists(nationalPark.Name))
            {
                ModelState.AddModelError("", "National Park Exists");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nationalp = _mapper.Map<NationalPark>(nationalPark);

            if (!_npRepository.CreateNationalPark(nationalp))
            {
                ModelState.AddModelError("", $"Something went wrong when saving record. {nationalp.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new { nationalParkId = nationalPark.Id }, nationalPark);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int id, [FromBody] NationalParkDTO nationalPark)
        {
            if (nationalPark == null || id != nationalPark.Id)
            {
                return BadRequest(ModelState);
            }

            var nationalp = _mapper.Map<NationalPark>(nationalPark);

            if (!_npRepository.UpdateNationalPark(nationalp))
            {
                ModelState.AddModelError("", $"Something went wrong when Updating record. {nationalp.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        public IActionResult DeleteNationalPark(int id)
        {
            if (!_npRepository.NationalParkExists(id))
            {
                return NotFound();
            }

            var nationalPark = _npRepository.GetNationalPark(id);

            if (!_npRepository.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong when Deleting record. {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
