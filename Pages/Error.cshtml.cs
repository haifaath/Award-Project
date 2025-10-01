using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using IAUResearcherAwardForm.Models;
using System.Text;

namespace IAUResearcherAwardForm.Pages
{
    /// <summary>
    /// Custom middleware for handling form validation errors
    /// </summary>
    public class ValidationErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ValidationErrorHandlerMiddleware> _logger;

        public ValidationErrorHandlerMiddleware(RequestDelegate next, ILogger<ValidationErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during request processing");
                
                // Redirect to error page for unhandled exceptions
                context.Response.Redirect("/Error");
            }
        }
    }

    /// <summary>
    /// Error page model for displaying application errors
    /// </summary>
    public class ErrorModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;

        [BindProperty]
        public string ErrorMessage { get; set; } = "An error occurred while processing your request.";

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Log that the error page was accessed
            _logger.LogWarning("Error page accessed at: {time}", DateTime.Now);
            
            // Check if there's a specific error message in TempData
            if (TempData["ErrorMessage"] != null)
            {
                ErrorMessage = TempData["ErrorMessage"].ToString();
            }
        }
    }

    /// <summary>
    /// Extension methods for validation error handling
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Gets all validation errors from ModelState as a formatted string
        /// </summary>
        public static string GetValidationErrors(this ModelStateDictionary modelState)
        {
            var errors = new StringBuilder();
            
            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors.AppendLine($"- {state.Key}: {error.ErrorMessage}");
                }
            }
            
            return errors.ToString();
        }
        
        /// <summary>
        /// Logs all validation errors from ModelState
        /// </summary>
        public static void LogValidationErrors(this ModelStateDictionary modelState, ILogger logger)
        {
            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    logger.LogWarning("Validation error for {Field}: {Error}", state.Key, error.ErrorMessage);
                }
            }
        }
    }
}
