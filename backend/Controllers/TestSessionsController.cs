using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestSessionsController : ControllerBase
    {
        private readonly TermService _termService;
        private readonly TestSessionService _testSessionService;

        public TestSessionsController(TermService termService, TestSessionService testSessionService)
        {
            _termService = termService;
            _testSessionService = testSessionService;
        }

        [HttpPost]
        public ActionResult<TestSession> CreateTestSession([FromBody] TestSessionDTO testSessionDto)
        {
            var terms = _termService.Terms.AsEnumerable();

            if (testSessionDto.CategoryIDs != null && testSessionDto.CategoryIDs.Any())
            {
                terms = terms.Where(t => t.CategoryIDs.Any(cId => testSessionDto.CategoryIDs.Contains(cId)));
            }

            terms = terms.OrderBy(_ => Guid.NewGuid()).Take(testSessionDto.SessionLength);

            var testSession = new TestSession
            {
                ID = Guid.NewGuid(),
                LanguageID = testSessionDto.LanguageID,
                CategoryIDs = testSessionDto.CategoryIDs ?? new List<Guid>(),
                TermIDs = terms.Select(t => t.ID).ToList(),
                SessionLength = terms.Count(),
                SessionAttemptCount = 0,
                SessionCorrectCount = 0,
                CreationDateTime = DateTime.UtcNow,
                IsComplete = false
            };

            _testSessionService.TestSessions.Add(testSession);

            return Ok(testSession);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TestSession>> GetAllTestSessions()
        {
            return Ok(_testSessionService.TestSessions);
        }

        [HttpGet("{id}")]
        public ActionResult<TestSession> GetTestSession(Guid id)
        {
            var testSession = _testSessionService.TestSessions.FirstOrDefault(ts => ts.ID == id);
            if (testSession == null)
            {
                return NotFound();
            }
            return Ok(testSession);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTestSession(Guid id, [FromBody] TestSessionDTO testSessionDto)
        {
            var testSession = _testSessionService.TestSessions.FirstOrDefault(ts => ts.ID == id);
            if (testSession == null)
            {
                return NotFound();
            }

            testSession.LanguageID = testSessionDto.LanguageID;
            testSession.CategoryIDs = testSessionDto.CategoryIDs;
            testSession.SessionLength = testSessionDto.SessionLength;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTestSession(Guid id)
        {
            var testSession = _testSessionService.TestSessions.FirstOrDefault(ts => ts.ID == id);
            if (testSession == null)
            {
                return NotFound();
            }

            _testSessionService.TestSessions.Remove(testSession);
            return NoContent();
        }
    }
}