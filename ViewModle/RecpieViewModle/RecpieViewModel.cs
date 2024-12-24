using Microsoft.Identity.Client;

namespace FoodApp.Api.ViewModle.RecpieViewModle
{
    public class RecpieCreateViewModel
    {
        public string Name { get; set; }    

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public  decimal price { get; set; }

        public List<int> TagId { get; set; }

    }
}
