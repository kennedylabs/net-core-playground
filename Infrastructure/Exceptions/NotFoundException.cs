using Microsoft.AspNetCore.Mvc;

namespace AccountsWebsite.Infrastructure.Exceptions
{
    public class NotFoundException : ActionResultException
    {
        public override IActionResult GetResult()
        {
            return new NotFoundResult();
        }
    }
}
