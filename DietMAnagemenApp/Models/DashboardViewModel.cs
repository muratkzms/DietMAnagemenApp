namespace DietMAnagemenApp.Models
{
    public class DashboardViewModel
    {
        public int ClientsCount { get; set; }
        public int DietplansCount { get; set; }
        public int MealsCount { get; set; }
        //public int UsersCount { get; set; }
        public List<Client> Clients { get; set; }
    }
}
