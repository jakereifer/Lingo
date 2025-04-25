using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private static readonly List<Category> Categories = new List<Category>();

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return Ok(Categories);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(Guid id)
        {
            var category = Categories.FirstOrDefault(c => c.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> AddCategory(CategoryDTO categoryDto)
        {
            if (string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                return BadRequest("Category name is required.");
            }

            var category = new Category
            {
                ID = Guid.NewGuid(),
                Name = categoryDto.Name,
                Notes = categoryDto.Notes,
                LanguageID = categoryDto.LanguageID
            };

            Categories.Add(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.ID }, category);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(Guid id, CategoryDTO categoryDto)
        {
            var category = Categories.FirstOrDefault(c => c.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                return BadRequest("Category name is required.");
            }

            category.Name = categoryDto.Name;
            category.Notes = categoryDto.Notes;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(Guid id)
        {
            var category = Categories.FirstOrDefault(c => c.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            Categories.Remove(category);
            return NoContent();
        }
    }
}