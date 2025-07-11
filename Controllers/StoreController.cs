using API_Shop_Online.Dto.v1.Article;
using API_Shop_Online.Dto.v1.Customer;
using API_Shop_Online.Dto.v1.Store;
using API_Shop_Online.Models;
using API_Shop_Online.Services.Article;
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

            var baseUrl = $"{Request.Scheme}://{Request.Host}/stores/";

            var lisDto = new List<StoreDto>();

            foreach (var article in response)
            {
                var dto = _mapper.Map<StoreDto>(article);

                // Asume que article.Image es solo el nombre del archivo
                dto.ImageUrl = !string.IsNullOrEmpty(article.Image)
                    ? baseUrl + article.Image
                    : null;

                lisDto.Add(dto);
            }

            return Ok(lisDto);
        }

        [HttpGet("{id}", Name = "GetStoreById")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Store))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var store = storeService.GetById(id);
            if (store == null)
                return NotFound();

            var dto = _mapper.Map<StoreDto>(store);

            dto.ImageUrl = string.IsNullOrEmpty(store.Image)
                ? null
                : $"{Request.Scheme}://{Request.Host}/stores/{store.Image}";

            return Ok(dto);
        }

        [HttpGet("{storeId}/articles/{articleId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StoreArticleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStoreArticle(int storeId, int articleId)
        {
            var result = await storeService.GetStoreArticle(storeId, articleId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{storeId}/articles")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StoreArticleDto>))]
        public async Task<IActionResult> GetArticlesByStore(int storeId)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}/images/";
            var result = await storeService.GetArticlesByStore(storeId, baseUrl);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromForm] StoreSubmissionDto request)
        {
            var response = await storeService.Create(request);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPost("{storeId}/articles")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StoreArticleDto))]
        public async Task<IActionResult> AddArticleToStore(int storeId, [FromBody] StoreArticleSubmissionDto request)
        {
            var result = await storeService.AddArticleToStore(storeId, request);
            return CreatedAtAction(nameof(GetStoreArticle), new { storeId = storeId, articleId = request.ArticleId }, result);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Store))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromForm] StoreSubmissionDto request)
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
