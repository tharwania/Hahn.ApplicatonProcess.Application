using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Web.Models.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hahn.ApplicatonProcess.December2020.Web.Filter
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorModalState = context.ModelState
                    .Where(modalState => modalState.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(error => error.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponse();
                foreach (var error in errorModalState)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModal = new ErrorModel()
                        {
                            FieldName = error.Key,
                            Message = subError
                        };
                        errorResponse.Errors.Add(errorModal);
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }
            await next();
        }
    }
}
