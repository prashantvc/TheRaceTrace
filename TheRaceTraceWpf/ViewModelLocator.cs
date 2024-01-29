using Microsoft.Extensions.DependencyInjection;
namespace TheRaceTraceWpf;
class ViewModelLocator
{
    public MainViewModel MainViewModel => App.Current.Services.GetService<MainViewModel>();
}
