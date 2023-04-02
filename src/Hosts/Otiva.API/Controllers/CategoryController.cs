using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.Category;
using Otiva.Contracts.CategoryDto;
using System.Net;

namespace Otiva.API.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //[AllowAnonymous]
        [Authorize]
        [HttpGet("category/all")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _categoryService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("category/getById/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdCategory(Guid id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }


        [HttpPost("category/create")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateCategoryAsync(string name)
        {
            var result = await _categoryService.CreateCategoryAsync(name);

            return Created("", new { result });
        }

        [HttpPut("category/put/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditCategoryAsync(Guid id, string name)
        {
            await _categoryService.EditCategoryAsync(id, name);
            return NoContent();
        }

        [HttpDelete("category/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
