using HotelListing.API.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace HotelListing.API.Core.Middleware;

/// <summary>
/// Remember Middleware has to be register in program.cs
/// </summary>
public class ExceptionMiddleware
{
	private readonly RequestDelegate next;
	private readonly ILogger<ExceptionMiddleware> logger;

	public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
	{
		this.next = next;
		this.logger = logger;
	}

	/// <summary>
	/// This method intercepts ALL OPERATIONS 
	/// So this TRY CATCH block is GLOBAL to the API
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			// this would log EVERYTHING
			//logger.LogInformation($"Intercepted operation {context.Request.Path} - {context.Request.Method}");
			await next(context);
		}
		catch(Exception ex)
		{
			logger.LogError(ex, $"Error when trying {context.Request.Path}");
			await HandleExceptionAsync(context, ex);
		}
	}

	private Task HandleExceptionAsync(HttpContext context, Exception ex)
	{
		context.Response.ContentType = "application/json";
		HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
		var errorDetails = new ErrorDetails
		{
			ErrorType = "Failure",
			ErrorMessage = ex.Message
		};

		switch(ex)
		{
			case NotFoundException notFoundEx:
				statusCode = HttpStatusCode.NotFound;
				errorDetails.ErrorType = "Not Found";
				break;
			default:
				break;
		}

		string response = JsonConvert.SerializeObject(errorDetails);
		context.Response.StatusCode = (int)statusCode;
		return context.Response.WriteAsync(response);
	}
}
