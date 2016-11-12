using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace AccountsWebsite.Infrastructure.Exceptions
{
    public class BadRequestObjectException : ActionResultException
    {
        public ModelStateDictionary ModelState { get; }

        public object Error { get; }

        public BadRequestObjectException(ModelStateDictionary modelState)
        {
            if (modelState == null)
                throw new ArgumentNullException(nameof(modelState));

            ModelState = modelState;
        }

        public BadRequestObjectException(object error)
        {
            if (error == null)
                throw new ArgumentNullException(nameof(error));

            Error = error;
        }

        public override IActionResult GetResult()
        {
            return ModelState != null ? new BadRequestObjectResult(ModelState) :
                new BadRequestObjectResult(Error);
        }
    }
}
