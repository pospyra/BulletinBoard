using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.Category;
using Otiva.Contracts.CategoryDto;
using System.Net;

namespace Otiva.API.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("category/all")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCategory(int take, int skip)
        {
            var result = await _categoryService.GetAll(take, skip);
            return Ok(result);
        }

        [HttpPost("category/create")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateCategoryAsync(string name)
        {
            var result = await _categoryService.CreateCategoryAsync(name);

            return Created("", new { });
        }

        [HttpPut("category/put/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditCategory(Guid id, string name)
        {
            await _categoryService.EditCategoryAsync(id, name);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
