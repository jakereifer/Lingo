using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TermsController : ControllerBase
    {
        private readonly TermService _termService;
        private readonly LanguageService _languageService;
        private readonly CategoryService _categoryService;

        public TermsController(TermService termService, LanguageService languageService, CategoryService categoryService)
        {
            _termService = termService;
            _languageService = languageService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Term>> GetTerms()
        {
            return Ok(_termService.Terms);
        }

        [HttpGet("{id}")]
        public ActionResult<Term> GetTerm(Guid id)
        {
            var term = _termService.Terms.FirstOrDefault(t => t.ID == id);
            if (term == null)
            {
                return NotFound();
            }
            return Ok(term);
        }

        [HttpPost]
        public ActionResult<Term> AddTerm(TermDTO termDto)
        {
            if (string.IsNullOrWhiteSpace(termDto.English) || string.IsNullOrWhiteSpace(termDto.Translation))
            {
                return BadRequest("Both English and Translation fields are required.");
            }

            if (!_languageService.Languages.Any(l => l.ID == termDto.LanguageID))
            {
                return BadRequest("Invalid LanguageID. The referenced language does not exist.");
            }

            if (termDto.CategoryIDs.Any(cId => !_categoryService.Categories.Any(c => c.ID == cId)))
            {
                return BadRequest("One or more CategoryIDs are invalid. The referenced categories do not exist.");
            }

            var term = new Term
            {
                ID = Guid.NewGuid(),
                English = termDto.English,
                Translation = termDto.Translation,
                Notes = termDto.Notes,
                LanguageID = termDto.LanguageID,
                CategoryIDs = termDto.CategoryIDs,
                CreationDateTime = DateTime.UtcNow,
                UpdatedDateTime = DateTime.UtcNow,
                TestAttemptCount = 0,
                TestCorrectCount = 0,
                Flag = termDto.Flag
            };

            _termService.Terms.Add(term);
            return CreatedAtAction(nameof(GetTerm), new { id = term.ID }, term);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTerm(Guid id, TermDTO termDto)
        {
            var term = _termService.Terms.FirstOrDefault(t => t.ID == id);
            if (term == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(termDto.English) || string.IsNullOrWhiteSpace(termDto.Translation))
            {
                return BadRequest("Both English and Translation fields are required.");
            }

            if (termDto.CategoryIDs.Any(cId => !_categoryService.Categories.Any(c => c.ID == cId)))
            {
                return BadRequest("One or more CategoryIDs are invalid. The referenced categories do not exist.");
            }

            term.English = termDto.English;
            term.Translation = termDto.Translation;
            term.Notes = termDto.Notes;
            term.CategoryIDs = termDto.CategoryIDs;
            term.Flag = termDto.Flag;
            term.UpdatedDateTime = DateTime.UtcNow;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTerm(Guid id)
        {
            var term = _termService.Terms.FirstOrDefault(t => t.ID == id);
            if (term == null)
            {
                return NotFound();
            }

            _termService.Terms.Remove(term);
            return NoContent();
        }

        [HttpGet("filter")]
        public ActionResult<IEnumerable<Term>> FilterTerms([FromQuery] Guid? categoryId, [FromQuery] bool? flag, [FromQuery] string? sortBy)
        {
            var terms = _termService.Terms.AsEnumerable();

            if (categoryId.HasValue)
            {
                terms = terms.Where(t => t.CategoryIDs.Contains(categoryId.Value));
            }

            if (flag.HasValue)
            {
                terms = terms.Where(t => t.Flag == flag.Value);
            }

            terms = sortBy?.ToLower() switch
            {
                "creation" => terms.OrderBy(t => t.CreationDateTime),
                "update" => terms.OrderBy(t => t.UpdatedDateTime),
                "performance" => terms.OrderByDescending(t => t.TestCorrectCount / (double)(t.TestAttemptCount == 0 ? 1 : t.TestAttemptCount)),
                _ => terms
            };

            return Ok(terms);
        }
    }
}