using Alliance_for_Life.Models;

namespace Alliance_for_Life.ViewModels
{
    public class BudgetViewModel
    {
        public BudgetCosts budgetcosts { get; set; }
        public AdminCosts admincost { get; set; }
        public ParticipationService particost { get; set; }
    }
}