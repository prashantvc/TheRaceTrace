using System.Windows;

namespace TheRaceTraceWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        void Season_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var vm = DataContext as MainViewModel;
            vm?.LoadRaceListCommand.Execute(null);
        }
    }
}