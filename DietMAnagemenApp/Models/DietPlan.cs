namespace DietMAnagemenApp.Models
{
    public class DietPlan: EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public float StartingWeight { get; set; }
        public int? ClientId { get; set; }
        public List<Meal>? Meals { get; set; } = new();
    }
}