
namespace HobbyHubSystem.Web.ViewModels.Category
{
    public class AllCategoriesViewModel
    {
        public AllCategoriesViewModel()
        {
            this.Categories = new List<CategoryViewModel>();
        }

        public List<CategoryViewModel> Categories { get; set; }
    }
}
