using Microsoft.Extensions.DependencyInjection;

class ViewModelLocator
{
    public MainViewModel MainViewModel => App.Current.Services.GetService<MainViewModel>();
}
