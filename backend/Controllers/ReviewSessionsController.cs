using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewSessionsController : ControllerBase
    {
        private readonly TermService _termService;

        public ReviewSessionsController(TermService termService)
        {
            _termService = termService;
        }

        [HttpPost]
        public ActionResult<IEnumerable<Term>> CreateReviewSession([FromQuery] int length, [FromQuery] List<Guid>? categoryIds)
        {
            var terms = _termService.Terms.AsEnumerable();

            if (categoryIds != null && categoryIds.Any())
            {
                terms = terms.Where(t => t.CategoryIDs.Any(cId => categoryIds.Contains(cId)));
            }

            terms = terms.OrderBy(_ => Guid.NewGuid()).Take(length);

            return Ok(terms);
        }
    }
}