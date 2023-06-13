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

        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCategory(CancellationToken cancellation)
        {
            var result = await _categoryService.GetAllAsync(cancellation);

            return Ok(result);
        }
        /// <summary>
        /// Получить категорию по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("category/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdCategory(Guid id, CancellationToken cancellation)
        {
            var result = await _categoryService.GetByIdAsync(id, cancellation);
            return Ok(result);
        }

        /// <summary>
        /// Добавить категорию
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("category")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateCategoryAsync(string name, CancellationToken cancellation)
        {
            var result = await _categoryService.CreateCategoryAsync(name, cancellation);

            return Created("", new { result });
        }

        /// <summary>
        /// Редактировать категорию
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("category/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditCategoryAsync(Guid id, string name, CancellationToken cancellation)
        {
            await _categoryService.EditCategoryAsync(id, name, cancellation);
            return NoContent();
        }

        /// <summary>
        /// Удалить категорию
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("category/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id, CancellationToken cancellation)
        {
            await _categoryService.DeleteAsync(id, cancellation);
            return NoContent();
        }
    }
}
