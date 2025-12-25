namespace FinalProjectPortfolio.Models
{
    public class PortfolioViewModel
    {
        public Profile? Profile { get; set; }
        public About? About { get; set; }
        public List<Experience> Experiences { get; set; } = new List<Experience>();
        public List<Project> Projects { get; set; } = new List<Project>();
        public Contact? Contact { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<SocialLink> SocialLinks { get; set; } = new List<SocialLink>();
        public List<Education> Educations { get; set; } = new List<Education>();
        public List<Service> Services { get; set; } = new List<Service>();
    }
}