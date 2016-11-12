using Microsoft.AspNetCore.Mvc;
using System;

namespace AccountsWebsite.Infrastructure.Exceptions
{
    public abstract class ActionResultException : Exception
    {
        public abstract IActionResult GetResult();
    }
}
