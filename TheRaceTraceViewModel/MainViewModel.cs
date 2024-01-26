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

    public RaceSummary? RaceSummary { get; private set; }

    public IAsyncRelayCommand LoadConstructorsCommand { get; }

    async Task GetConstructorsAsync()
    {
        var summary = await _ergastService.RaceSummaryAsync();
        RaceSummary = summary;
    }

    readonly IErgastService _ergastService;
}
