using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IAUResearcherAwardForm.Models
{
    /// <summary>
    /// Custom validation attribute to ensure the date of joining is not in the future
    /// </summary>
    public class NotFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date > DateTime.Today)
                {
                    return new ValidationResult("Date cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validation attribute to ensure the publication year is within the last 5 years
    /// </summary>
    public class ValidPublicationYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int year)
            {
                int currentYear = DateTime.Today.Year;
                if (year < currentYear - 5 || year > currentYear)
                {
                    return new ValidationResult($"Publication year must be within the last 5 years ({currentYear - 5} to {currentYear}).");
                }
            }
            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validation attribute to ensure the researcher has at least 2 years of service at IAU
    /// </summary>
    public class MinimumServiceYearsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfJoining)
            {
                TimeSpan serviceTime = DateTime.Today - dateOfJoining;
                if (serviceTime.TotalDays < 730) // 2 years = 730 days
                {
                    return new ValidationResult("Minimum of 2-year service at IAU is required.");
                }
            }
            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Model class for the IAU Best Researcher Award application form.
    /// This class contains all the properties needed to capture researcher information
    /// and research accomplishments.
    /// </summary>
    public class ResearcherModel
    {
        // Section 2: Researcher Information
        [Required(ErrorMessage = "Please select your affiliation")]
        [Display(Name = "Researcher Affiliation")]
        public string Affiliation { get; set; }

        [Required(ErrorMessage = "Researcher name is required")]
        [Display(Name = "Researcher Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Discipline/Subject Category is required")]
        [Display(Name = "Discipline/Subject Category")]
        [StringLength(100, ErrorMessage = "Discipline must not exceed 100 characters")]
        public string Discipline { get; set; }

        [Required(ErrorMessage = "Present Appointment is required")]
        [Display(Name = "Present Appointment")]
        [StringLength(100, ErrorMessage = "Present Appointment must not exceed 100 characters")]
        public string PresentAppointment { get; set; }

        [Required(ErrorMessage = "Date of Joining IAU is required")]
        [Display(Name = "Date of Joining IAU")]
        [DataType(DataType.Date)]
        [NotFutureDate(ErrorMessage = "Date of joining cannot be in the future")]
        [MinimumServiceYears(ErrorMessage = "Minimum of 2-year service at IAU is required")]
        public DateTime DateOfJoining { get; set; }

        [Required(ErrorMessage = "Please select how long you have been active in research")]
        [Display(Name = "How long have you been active in research")]
        public string ResearchActiveYears { get; set; }

        [Required(ErrorMessage = "Please select your highest qualification")]
        [Display(Name = "Highest Qualification")]
        public string HighestQualification { get; set; }

        [Required(ErrorMessage = "Area of Specialisation/Research Interest is required")]
        [Display(Name = "Area of Specialisation/Research Interest")]
        [StringLength(500, ErrorMessage = "Area of Specialisation must not exceed 500 characters")]
        public string SpecialisationArea { get; set; }

        // Section 3: Research Profile
        // A. Scientific Publications
        public List<ScientificPublication> ScientificPublications { get; set; } = new List<ScientificPublication>();

        // B. Nature or Science Publications
        public List<NatureSciencePublication> NatureSciencePublications { get; set; } = new List<NatureSciencePublication>();

        // C. Books
        public List<Book> Books { get; set; } = new List<Book>();

        // Scientific Research Abstracts
        public List<ResearchAbstract> ResearchAbstracts { get; set; } = new List<ResearchAbstract>();

        // IAU Excellence Awards
        public List<ExcellenceAward> ExcellenceAwards { get; set; } = new List<ExcellenceAward>();

        // Research Grants
        public List<ResearchGrant> ResearchGrants { get; set; } = new List<ResearchGrant>();

        // Patents
        public List<Patent> Patents { get; set; } = new List<Patent>();

        // Research-Related Prizes/Awards
        public List<ResearchPrize> ResearchPrizes { get; set; } = new List<ResearchPrize>();

        // Students' Research Supervision
        public List<StudentSupervision> StudentSupervisions { get; set; } = new List<StudentSupervision>();

        // Other Scientific Excellence Related Practices
        public List<OtherScientificExcellence> OtherScientificExcellences { get; set; } = new List<OtherScientificExcellence>();
    }

    /// <summary>
    /// Model for scientific publications
    /// </summary>
    public class ScientificPublication
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Publication title is required")]
        [Display(Name = "Publication Title")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Publication title must be between 5 and 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Journal title is required")]
        [Display(Name = "Journal Title")]
        [StringLength(200, ErrorMessage = "Journal title must not exceed 200 characters")]
        public string JournalTitle { get; set; }

        [Required(ErrorMessage = "Quartile is required")]
        [Display(Name = "Quartile")]
        [RegularExpression("^(Q1|Q2|Q3|Q4)$", ErrorMessage = "Quartile must be Q1, Q2, Q3, or Q4")]
        public string Quartile { get; set; }

        [Required(ErrorMessage = "Publication year is required")]
        [Display(Name = "Publication Year")]
        [Range(2020, 2025, ErrorMessage = "Publication year must be between 2020 and 2025")]
        [ValidPublicationYear(ErrorMessage = "Publication year must be within the last 5 years")]
        public int PublicationYear { get; set; }

        [Required(ErrorMessage = "Author list is required")]
        [Display(Name = "Author List")]
        [StringLength(500, ErrorMessage = "Author list must not exceed 500 characters")]
        public string AuthorList { get; set; }

        [Display(Name = "Paper Link")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string PaperLink { get; set; }

        [Required(ErrorMessage = "Author contribution is required")]
        [Display(Name = "Author Contribution")]
        public string AuthorContribution { get; set; }
    }

    /// <summary>
    /// Model for Nature or Science publications
    /// </summary>
    public class NatureSciencePublication
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Publication title is required")]
        [Display(Name = "Publication Title")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Publication title must be between 5 and 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Journal title is required")]
        [Display(Name = "Journal Title")]
        [StringLength(200, ErrorMessage = "Journal title must not exceed 200 characters")]
        public string JournalTitle { get; set; }

        [Required(ErrorMessage = "Quartile is required")]
        [Display(Name = "Quartile")]
        [RegularExpression("^(Q1|Q2|Q3|Q4)$", ErrorMessage = "Quartile must be Q1, Q2, Q3, or Q4")]
        public string Quartile { get; set; }

        [Required(ErrorMessage = "Publication year is required")]
        [Display(Name = "Publication Year")]
        [Range(2020, 2025, ErrorMessage = "Publication year must be between 2020 and 2025")]
        [ValidPublicationYear(ErrorMessage = "Publication year must be within the last 5 years")]
        public int PublicationYear { get; set; }

        [Required(ErrorMessage = "Author list is required")]
        [Display(Name = "Author List")]
        [StringLength(500, ErrorMessage = "Author list must not exceed 500 characters")]
        public string AuthorList { get; set; }

        [Display(Name = "Paper Link")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string PaperLink { get; set; }

        [Required(ErrorMessage = "Author contribution is required")]
        [Display(Name = "Author Contribution")]
        public string AuthorContribution { get; set; }
    }

    /// <summary>
    /// Model for books
    /// </summary>
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required")]
        [Display(Name = "Book Title")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Book title must be between 3 and 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        [Display(Name = "Publisher")]
        [StringLength(100, ErrorMessage = "Publisher must not exceed 100 characters")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Display(Name = "Year")]
        [Range(2020, 2025, ErrorMessage = "Year must be between 2020 and 2025")]
        [ValidPublicationYear(ErrorMessage = "Year must be within the last 5 years")]
        public int Year { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [Display(Name = "ISBN")]
        [RegularExpression(@"^(?:ISBN(?:-1[03])?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$", 
            ErrorMessage = "Please enter a valid ISBN")]
        public string ISBN { get; set; }

        [Display(Name = "Link or PDF")]
        public string LinkOrPdf { get; set; }
    }

    /// <summary>
    /// Model for research abstracts
    /// </summary>
    public class ResearchAbstract
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Research abstract title is required")]
        [Display(Name = "Research Abstract Title")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Research abstract title must be between 5 and 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Scientific Conference details are required")]
        [Display(Name = "Scientific Conference Name, Location and Date")]
        [StringLength(300, ErrorMessage = "Conference details must not exceed 300 characters")]
        public string ConferenceDetails { get; set; }

        [Required(ErrorMessage = "Investigator(s) is required")]
        [Display(Name = "Investigator(s)")]
        [StringLength(300, ErrorMessage = "Investigators must not exceed 300 characters")]
        public string Investigators { get; set; }

        [Required(ErrorMessage = "Type of Participation is required")]
        [Display(Name = "Type of Participation (Oral, Poster)")]
        [RegularExpression("^(Oral|Poster)$", ErrorMessage = "Participation type must be either 'Oral' or 'Poster'")]
        public string ParticipationType { get; set; }

        [Display(Name = "Abstract Link")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string AbstractLink { get; set; }
    }

    /// <summary>
    /// Model for excellence awards
    /// </summary>
    public class ExcellenceAward
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Award details are required")]
        [Display(Name = "Award Details")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Award details must be between 5 and 300 characters")]
        public string AwardDetails { get; set; }

        [Required(ErrorMessage = "Award class is required")]
        [Display(Name = "Award Class")]
        [RegularExpression("^(A-class|B-class|C-class)$", ErrorMessage = "Award class must be A-class, B-class, or C-class")]
        public string AwardClass { get; set; }
    }

    /// <summary>
    /// Model for research grants
    /// </summary>
    public class ResearchGrant
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Grant title is required")]
        [Display(Name = "Grant Title")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Grant title must be between 5 and 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Awarding body is required")]
        [Display(Name = "Awarding Body")]
        [StringLength(100, ErrorMessage = "Awarding body must not exceed 100 characters")]
        public string AwardingBody { get; set; }

        [Required(ErrorMessage = "Grant type is required")]
        [Display(Name = "Grant Type (External/Internal/Other)")]
        [RegularExpression("^(External|Internal|Other)$", ErrorMessage = "Grant type must be External, Internal, or Other")]
        public string GrantType { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [Display(Name = "PI, Co-PI or Contributor Researcher")]
        [RegularExpression("^(PI|Co-PI|Contributor Researcher)$", ErrorMessage = "Role must be PI, Co-PI, or Contributor Researcher")]
        public string Role { get; set; }
    }

    /// <summary>
    /// Model for patents
    /// </summary>
    public class Patent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Patent details are required")]
        [Display(Name = "Patent Details")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Patent details must be between 10 and 500 characters")]
        public string PatentDetails { get; set; }

        [Required(ErrorMessage = "Inventor role is required")]
        [Display(Name = "Inventor Role")]
        [RegularExpression("^(First Inventor|Co-Inventor)$", ErrorMessage = "Inventor role must be First Inventor or Co-Inventor")]
        public string InventorRole { get; set; }
    }

    /// <summary>
    /// Model for research prizes/awards
    /// </summary>
    public class ResearchPrize
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Prize/Award details are required")]
        [Display(Name = "Prize/Award Details")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Prize/Award details must be between 10 and 500 characters")]
        public string PrizeDetails { get; set; }
    }

    /// <summary>
    /// Model for student supervision
    /// </summary>
    public class StudentSupervision
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Supervision details are required")]
        [Display(Name = "Supervision Details")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Supervision details must be between 10 and 500 characters")]
        public string SupervisionDetails { get; set; }

        [Required(ErrorMessage = "Supervision type is required")]
        [Display(Name = "Supervision Type")]
        [RegularExpression("^(Ph\\.D\\. thesis|Master's thesis|Bachelor dissertation|Others)$", 
            ErrorMessage = "Supervision type must be Ph.D. thesis, Master's thesis, Bachelor dissertation, or Others")]
        public string SupervisionType { get; set; }
    }

    /// <summary>
    /// Model for other scientific excellence
    /// </summary>
    public class OtherScientificExcellence
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Achievement details are required")]
        [Display(Name = "Achievement Details")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Achievement details must be between 10 and 500 characters")]
        public string AchievementDetails { get; set; }
    }
}
