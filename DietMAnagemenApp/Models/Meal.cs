namespace DietMAnagemenApp.Models
{
    public class Meal: EntityBase
    {
        public string Name { get; set; }
        public string Content { get; set; } = "Default Content Of a Meal";
        public TimeOnly MealTime { get; set; } = new TimeOnly(9, 30);
        public int DietPlanId { get; set; } = 1;
    }
}