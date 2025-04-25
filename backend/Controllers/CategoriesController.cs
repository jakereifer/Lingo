using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Lingo.Controllers;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly LanguageService _languageService;

        public CategoriesController(CategoryService categoryService, LanguageService languageService)
        {
            _categoryService = categoryService;
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return Ok(_categoryService.Categories);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(Guid id)
        {
            var category = _categoryService.Categories.FirstOrDefault(c => c.ID == id);
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

            if (!_languageService.Languages.Any(l => l.ID == categoryDto.LanguageID))
            {
                return BadRequest("Invalid LanguageID. The referenced language does not exist.");
            }

            var category = new Category
            {
                ID = Guid.NewGuid(),
                Name = categoryDto.Name,
                Notes = categoryDto.Notes,
                LanguageID = categoryDto.LanguageID
            };

            _categoryService.Categories.Add(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.ID }, category);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(Guid id, CategoryDTO categoryDto)
        {
            var category = _categoryService.Categories.FirstOrDefault(c => c.ID == id);
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
            var category = _categoryService.Categories.FirstOrDefault(c => c.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            _categoryService.Categories.Remove(category);
            return NoContent();
        }
    }
}