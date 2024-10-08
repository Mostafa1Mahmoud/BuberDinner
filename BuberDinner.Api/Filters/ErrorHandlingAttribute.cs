using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberDinner.Api.Filters;

public class ErrorHandlingAttribute: ExceptionFilterAttribute{
    public override void OnException(ExceptionContext context){
        
        if(context.Exception is null)
            return;
        
        var problemDetails = new ProblemDetails{
            Title = $"An error occurred while processing your request: {context.Exception.Message}",
            Status = (int)HttpStatusCode.InternalServerError
        };

        context.Result = new ObjectResult(problemDetails);

        context.ExceptionHandled = true;
    }
} 