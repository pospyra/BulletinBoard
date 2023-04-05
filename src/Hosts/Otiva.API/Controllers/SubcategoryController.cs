using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.Category;
using Otiva.AppServeces.Service.Subcategory;
using Otiva.Contracts.CategoryDto;
using System.Net;

namespace Otiva.API.Controllers
{
    [ApiController]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryService _subcategoryService;

        public SubcategoryController(ISubcategoryService subcategoryService)
        {
            _subcategoryService = subcategoryService;
        }

        [HttpGet("subcategory/all")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllSubategory(CancellationToken cancellation)
        {
            var result = await _subcategoryService.GetAllAsync( cancellation);
            return Ok(result);
        }

        [HttpPost("subcategory/create")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateSubcategoryAsync(string name, Guid categoryId, CancellationToken cancellation)
        {
            var result = await _subcategoryService.CreateSubCategoryAsync(name, categoryId, cancellation);

            return Created("", new { result});
        }

        [HttpPut("subcategory/put/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditSubategory(Guid id, string name, Guid categoryId, CancellationToken cancellation)
        {
            await _subcategoryService.EditSubCategoryAsync(id, name, categoryId, cancellation);
            return NoContent();
        }

        [HttpDelete("subcategory/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id, CancellationToken cancellation)
        {
            await _subcategoryService.DeleteAsync(id, cancellation);
            return NoContent();
        }
    }
}
