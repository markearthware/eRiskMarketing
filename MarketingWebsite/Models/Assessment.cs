namespace MarketingWebsite.Models
{
    public class Assessment
    {
        public int AssessmentId { get; set; }

        public string Controls { get; set; }

        public string ExistingControls { get; set; }

        public string FurtherDetails { get; set; }

        public string Hazard { get; set; }

        public string How { get; set; }

        public int Likelihood { get; set; }

        public int LikelihoodB { get; set; }

        public int Severity { get; set; }

        public int SeverityB { get; set; }

        public string Who { get; set; }

        public virtual Task Task { get; set; }
    }
}