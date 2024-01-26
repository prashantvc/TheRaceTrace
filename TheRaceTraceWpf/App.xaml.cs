using System.Windows;
using ErgastLib;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace TheRaceTraceWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Initialize the Refit client
            var ergastApi = RestService.For<IErgastApi>("http://ergast.com/api/f1");

            // Add the Refit client to the services
            services.AddSingleton(ergastApi);

            services.AddSingleton<IErgastService, ErgastService>();
            services.AddTransient<MainViewModel>();


            return services.BuildServiceProvider();
        }

    }
}
