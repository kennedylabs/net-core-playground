using Microsoft.AspNetCore.Mvc;

namespace AccountsWebsite.Infrastructure.Exceptions
{
    public class BadRequestException : ActionResultException
    {
        public override IActionResult GetResult()
        {
            return new BadRequestResult();
        }
    }
}
