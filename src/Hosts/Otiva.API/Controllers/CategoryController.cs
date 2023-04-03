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
        [Authorize(Roles = "Admin")]
        [HttpGet("category/all")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _categoryService.GetAllAsync();

            return Ok(result);
        }
        /// <summary>
        /// Получить категорию по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("category/getById/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdCategory(Guid id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Добавить категорию
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("category/create")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateCategoryAsync(string name)
        {
            var result = await _categoryService.CreateCategoryAsync(name);

            return Created("", new { result });
        }

        /// <summary>
        /// Редактировать категорию
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut("category/put/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditCategoryAsync(Guid id, string name)
        {
            await _categoryService.EditCategoryAsync(id, name);
            return NoContent();
        }

        /// <summary>
        /// Удалить категорию
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
