using IAUResearcherAwardForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IAUResearcherAwardForm.Pages
{
    /// <summary>
    /// Page model for the IAU Best Researcher Award application form.
    /// This class handles the form submission and validation.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public ResearcherModel Researcher { get; set; } = new ResearcherModel();

        // Lists for dropdown options
        public List<string> AffiliationOptions { get; set; }
        public List<string> ResearchActiveYearsOptions { get; set; }
        public List<string> QualificationOptions { get; set; }
        public List<string> AuthorContributionOptions { get; set; }
        public List<string> NatureAuthorContributionOptions { get; set; }

        // Success message flag
        [TempData]
        public string SuccessMessage { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            
            // Initialize dropdown options
            InitializeDropdownOptions();
        }

        public void OnGet()
        {
            // Initialize with empty lists for form sections that allow multiple entries
            InitializeFormLists();
        }

        public IActionResult OnPost()
        {
            // Re-initialize dropdown options for the view
            InitializeDropdownOptions();

            if (!ModelState.IsValid)
            {
                // If model state is invalid, return the page with validation errors
                _logger.LogWarning("Form validation failed");
                
                // Ensure lists are initialized even when validation fails
                EnsureListsAreInitialized();
                
                return Page();
            }

            try
            {
                // In a real application, you would save the form data to a database here
                // For this example, we'll just log the submission and redirect to a success page
                
                // Log successful submission
                _logger.LogInformation("Form submitted successfully for researcher: {Name}", Researcher.Name);
                
                // Set success message
                SuccessMessage = $"Application for {Researcher.Name} submitted successfully!";
                
                // Redirect to a success page
                return RedirectToPage("./Success");
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error occurred while processing form submission");
                
                // Add error to model state
                ModelState.AddModelError(string.Empty, "An error occurred while processing your application. Please try again.");
                
                // Ensure lists are initialized
                EnsureListsAreInitialized();
                
                return Page();
            }
        }

        /// <summary>
        /// Initializes all form lists with at least one empty item
        /// </summary>
        private void InitializeFormLists()
        {
            Researcher.ScientificPublications = new List<ScientificPublication> { new ScientificPublication() };
            Researcher.NatureSciencePublications = new List<NatureSciencePublication> { new NatureSciencePublication() };
            Researcher.Books = new List<Book> { new Book() };
            Researcher.ResearchAbstracts = new List<ResearchAbstract> { new ResearchAbstract() };
            Researcher.ExcellenceAwards = new List<ExcellenceAward> { new ExcellenceAward() };
            Researcher.ResearchGrants = new List<ResearchGrant> { new ResearchGrant() };
            Researcher.Patents = new List<Patent> { new Patent() };
            Researcher.ResearchPrizes = new List<ResearchPrize> { new ResearchPrize() };
            Researcher.StudentSupervisions = new List<StudentSupervision> { new StudentSupervision() };
            Researcher.OtherScientificExcellences = new List<OtherScientificExcellence> { new OtherScientificExcellence() };
        }

        /// <summary>
        /// Ensures that all lists are initialized, creating empty lists if they are null
        /// </summary>
        private void EnsureListsAreInitialized()
        {
            Researcher.ScientificPublications ??= new List<ScientificPublication> { new ScientificPublication() };
            Researcher.NatureSciencePublications ??= new List<NatureSciencePublication> { new NatureSciencePublication() };
            Researcher.Books ??= new List<Book> { new Book() };
            Researcher.ResearchAbstracts ??= new List<ResearchAbstract> { new ResearchAbstract() };
            Researcher.ExcellenceAwards ??= new List<ExcellenceAward> { new ExcellenceAward() };
            Researcher.ResearchGrants ??= new List<ResearchGrant> { new ResearchGrant() };
            Researcher.Patents ??= new List<Patent> { new Patent() };
            Researcher.ResearchPrizes ??= new List<ResearchPrize> { new ResearchPrize() };
            Researcher.StudentSupervisions ??= new List<StudentSupervision> { new StudentSupervision() };
            Researcher.OtherScientificExcellences ??= new List<OtherScientificExcellence> { new OtherScientificExcellence() };
        }

        /// <summary>
        /// Initializes all dropdown options for the form
        /// </summary>
        private void InitializeDropdownOptions()
        {
            // Initialize affiliation options based on requirements
            AffiliationOptions = new List<string>
            {
                "Office of the Vice President for Administrative and Financial Affairs",
                "Office of the Vice President for Academic Affairs",
                "College of Medicine",
                "College of Dentistry",
                "College of Nursing",
                "College of Applied Medical Sciences",
                "College of Clinical Pharmacy",
                "College of Public Health",
                "College of Applied Medical Sciences – Jubail",
                "College of Architecture and Planning",
                "College of Design",
                "College of Engineering",
                "College of Applied Studies and Community Service",
                "College of Business Administration",
                "College of Computer Science and Information Technology",
                "College of Science",
                "Applied College",
                "College of Arts",
                "College of Education",
                "College of Science and Humanities – Jubail",
                "College of Sharia and Law",
                "Deanship of Preparatory year",
                "Office of the Vice President for Scientific Research and Innovation",
                "Office of the Vice President for Development and Community Partnership"
            };

            // Initialize research active years options
            ResearchActiveYearsOptions = new List<string>
            {
                "<10 years",
                "10-15 years",
                "15-20 years",
                ">20 years"
            };

            // Initialize qualification options
            QualificationOptions = new List<string>
            {
                "Master",
                "PhD"
            };

            // Initialize author contribution options for scientific publications
            AuthorContributionOptions = new List<string>
            {
                "Q1 and first author/corresponding author",
                "Q1 and co-author",
                "Q2 and first author/corresponding author",
                "Q2 and co-author"
            };

            // Initialize author contribution options for Nature/Science publications
            NatureAuthorContributionOptions = new List<string>
            {
                "Corresponding author affiliation",
                "First author affiliation (second author affiliation if the first author affiliation is the same as corresponding author affiliation)",
                "Next author affiliation",
                "Other author affiliations"
            };
        }
    }
}
