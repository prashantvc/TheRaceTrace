using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ErgastLib;
using System.Diagnostics;

public partial class MainViewModel : ObservableObject
{

    public MainViewModel(IErgastService ergastService)
    {
        _ergastService = ergastService;
        LoadConstructorsCommand = new AsyncRelayCommand(GetConstructorsAsync);
    }

    [ObservableProperty]
    private string? name;

    public IAsyncRelayCommand LoadConstructorsCommand { get; }

    async Task GetConstructorsAsync()
    {
        var laps = await _ergastService.RaceOverviewAsync();
        Debug.WriteLine(laps);
    }

    readonly IErgastService _ergastService;
}
