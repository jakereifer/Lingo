using Microsoft.AspNetCore.Mvc;
using Lingo.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Lingo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguagesController : ControllerBase
    {
        private static readonly List<Language> Languages = new();

        [HttpGet]
        public ActionResult<IEnumerable<Language>> GetLanguages()
        {
            return Ok(Languages);
        }

        [HttpGet("{id}")]
        public ActionResult<Language> GetLanguage(Guid id)
        {
            var language = Languages.FirstOrDefault(l => l.ID == id);
            if (language == null)
            {
                return NotFound();
            }
            return Ok(language);
        }

        [HttpPost]
        public ActionResult<Language> AddLanguage(LanguageDTO languageDto)
        {
            if (string.IsNullOrWhiteSpace(languageDto.LanguageName))
            {
                return BadRequest("Language name is required.");
            }

            var language = new Language
            {
                ID = Guid.NewGuid(),
                LanguageName = languageDto.LanguageName
            };

            Languages.Add(language);
            return CreatedAtAction(nameof(GetLanguage), new { id = language.ID }, language);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateLanguage(Guid id, LanguageDTO languageDto)
        {
            var language = Languages.FirstOrDefault(l => l.ID == id);
            if (language == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(languageDto.LanguageName))
            {
                return BadRequest("Language name is required.");
            }

            language.LanguageName = languageDto.LanguageName;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLanguage(Guid id)
        {
            var language = Languages.FirstOrDefault(l => l.ID == id);
            if (language == null)
            {
                return NotFound();
            }

            Languages.Remove(language);
            return NoContent();
        }
    }
}