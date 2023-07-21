
namespace HobbyHubSystem.Web.ViewModels.Hobby
{
    public class AllHobbiesViewModel
    {
        public AllHobbiesViewModel()
        {
            this.Hobbies = new List<HobbiesInCategoryViewModel>();
        }
        public List<HobbiesInCategoryViewModel> Hobbies { get; set; }
    }
}
