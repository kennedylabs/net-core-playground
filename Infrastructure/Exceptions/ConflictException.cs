using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountsWebsite.Infrastructure.Exceptions
{
    public class ConflictException : ActionResultException
    {
        public override IActionResult GetResult()
        {
            return new StatusCodeResult(StatusCodes.Status409Conflict);
        }
    }
}
