namespace DietMAnagemenApp.Models
{
    public class Client:EntityBase
    {
        public string Picture { get; set; }
        public string About { get; set; }
        public string Fullname { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<DietPlan>? DietPlans { get; set; } = new();
    }
}
