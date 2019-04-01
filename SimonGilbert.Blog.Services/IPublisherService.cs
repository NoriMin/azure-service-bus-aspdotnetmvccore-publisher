using System.Threading.Tasks;
using SimonGilbert.Blog.ViewModels;

namespace SimonGilbert.Blog.Services
{
    public interface IPublisherService
    {
        Task Send(RestaurantOrderViewModel viewModel);
    }
}
