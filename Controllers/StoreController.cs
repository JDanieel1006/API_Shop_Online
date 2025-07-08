using API_Shop_Online.Dto.v1.Customer;
using API_Shop_Online.Dto.v1.Store;
using API_Shop_Online.Models;
using API_Shop_Online.Services.Customers;
using API_Shop_Online.Services.Store;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Shop_Online.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/stores")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService storeService;
        private readonly IMapper _mapper;

        public StoreController(IStoreService ctRepo, IMapper mapper)
        {
            storeService = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var response = storeService.Get();

            var lisDto = new List<StoreDto>();

            foreach (var lista in response)
            {
                lisDto.Add(_mapper.Map<StoreDto>(lista));
            }
            return Ok(lisDto);
        }

        [HttpGet("{id}", Name = "GetStoreById")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            return Ok(storeService.GetById(id));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] StoreSubmissionDto request)
        {
            var response = await storeService.Create(request);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] StoreSubmissionDto request)
        {
            var result = await storeService.Update(id, request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            storeService.Delete(id);

            return NoContent();
        }
    }
}
