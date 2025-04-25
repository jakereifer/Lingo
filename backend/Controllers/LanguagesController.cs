using Microsoft.AspNetCore.Mvc;
using Lingo.Models;
using backend.Services;

namespace Lingo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguagesController : ControllerBase
    {
        private readonly LanguageService _languageService;

        public LanguagesController(LanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Language>> GetLanguages()
        {
            return Ok(_languageService.Languages);
        }

        [HttpGet("{id}")]
        public ActionResult<Language> GetLanguage(Guid id)
        {
            var language = _languageService.Languages.FirstOrDefault(l => l.ID == id);
            if (language == null)
            {
                return NotFound();
            }
            return Ok(language);
        }

        [HttpPost]
        public ActionResult<Language> AddLanguage(LanguageDTO languageDto)
        {
            if (string.IsNullOrWhiteSpace(languageDto.Name))
            {
                return BadRequest("Language name is required.");
            }

            var language = new Language
            {
                ID = Guid.NewGuid(),
                Name = languageDto.Name
            };

            _languageService.Languages.Add(language);
            return CreatedAtAction(nameof(GetLanguage), new { id = language.ID }, language);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateLanguage(Guid id, LanguageDTO languageDto)
        {
            var language = _languageService.Languages.FirstOrDefault(l => l.ID == id);
            if (language == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(languageDto.Name))
            {
                return BadRequest("Language name is required.");
            }

            language.Name = languageDto.Name;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLanguage(Guid id)
        {
            var language = _languageService.Languages.FirstOrDefault(l => l.ID == id);
            if (language == null)
            {
                return NotFound();
            }

            _languageService.Languages.Remove(language);
            return NoContent();
        }
    }
}