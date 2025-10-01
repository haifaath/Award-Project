using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IAUResearcherAwardForm.Pages
{
    /// <summary>
    /// Page model for the success page displayed after form submission.
    /// </summary>
    public class SuccessModel : PageModel
    {
        private readonly ILogger<SuccessModel> _logger;

        public SuccessModel(ILogger<SuccessModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Success page displayed at: {time}", DateTime.Now);
        }
    }
}
