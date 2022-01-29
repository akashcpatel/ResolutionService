using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Main
{
    public class ExceptionFilter : IExceptionFilter
    {
        private delegate IActionResult CreateResultObject(ExceptionContext context);

        private readonly Dictionary<Type, Func<ExceptionContext, IActionResult>> _createResultObject =
            new Dictionary<Type, Func<ExceptionContext, IActionResult>>
        {
            {typeof(ArgumentException), CreateBadRequestObjectResult }
        };

        public void OnException(ExceptionContext context)
        {
            var resultObject = _createResultObject.ContainsKey(context.Exception.GetType()) ?
                _createResultObject[typeof(ArgumentException)](context) : CreateInternalServerErrorResult(context);

            context.Result = resultObject;
            context.ExceptionHandled = true;
        }

        internal static IActionResult CreateBadRequestObjectResult(ExceptionContext context)
        {
            return new BadRequestObjectResult(context.Exception.Message);
        }

        internal static IActionResult CreateInternalServerErrorResult(ExceptionContext context)
        {
            return new ObjectResult(context.Exception.Message)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}