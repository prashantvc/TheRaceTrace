using Microsoft.Extensions.DependencyInjection;

namespace TheRaceTraceWpf
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.Current.Services.GetService<MainViewModel>();
    }
}
