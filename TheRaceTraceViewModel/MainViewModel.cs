using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ErgastLib;
using OxyPlot;
using OxyPlot.Series;
using System.Diagnostics;

public partial class MainViewModel : ObservableObject
{

    public MainViewModel(IErgastService ergastService)
    {
        _ergastService = ergastService;
        _raceTrace = new RaceTrace();

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

        var series = _raceTrace.CreateTraces(summary.DriverLapTimes);
        Debug.WriteLine(series.Count);

        var data = series.SelectMany(p => p.DataPoint.Select(dp => new { p.Driver, dp.Lap, dp.Time }));


        var tl = data.GroupBy(p => p.Driver).Select(g => new LineSeries
        {
            Title = $"{g.Key.PermanentNumber} - {g.Key.Code}",
            ItemsSource = g.OrderBy(p => p.Lap).Select(p => new DataPoint(p.Lap, p.Time)),

        });

        PlotModel = new() { Title = "Race Trace" };
        foreach (var item in tl)
        {
            PlotModel.Series.Add(item);
        }

        OnPropertyChanged(nameof(PlotModel));
    }

    public PlotModel PlotModel { get; private set; } = new();



    readonly IErgastService _ergastService;
    readonly RaceTrace _raceTrace;
}
