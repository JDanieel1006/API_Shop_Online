using API_Shop_Online.Dto.v1.Article;
using API_Shop_Online.Dto.v1.Store;
using API_Shop_Online.Models;
using API_Shop_Online.Services.Article;
using API_Shop_Online.Services.Store;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Shop_Online.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IMapper _mapper;

        public ArticleController(IArticleService ctRepo, IMapper mapper)
        {
            articleService = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var response = articleService.Get();

            var baseUrl = $"{Request.Scheme}://{Request.Host}/images/";

            var lisDto = new List<ArticleDto>();

            foreach (var article in response)
            {
                var dto = _mapper.Map<ArticleDto>(article);

                // Asume que article.Image es solo el nombre del archivo
                dto.ImageUrl = !string.IsNullOrEmpty(article.Image)
                    ? baseUrl + article.Image
                    : null;

                lisDto.Add(dto);
            }

            return Ok(lisDto);

        }

        [HttpGet("{id}", Name = "GetArticleById")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var article = articleService.GetById(id);
            if (article == null)
                return NotFound();

            var dto = _mapper.Map<ArticleDto>(article);

            dto.ImageUrl = string.IsNullOrEmpty(article.Image)
                ? null
                : $"{Request.Scheme}://{Request.Host}/images/{article.Image}";

            return Ok(dto);
        }


        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromForm] ArticleSubmissionDto request)
        {
            var response = await articleService.Create(request);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromForm] ArticleSubmissionDto request)
        {
            var result = await articleService.Update(id, request);

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
            articleService.Delete(id);

            return NoContent();
        }
    }
}
