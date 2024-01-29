using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ErgastLib;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System.Diagnostics;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel(IErgastService ergastService)
    {
        _ergastService = ergastService;
        _raceTrace = new RaceTrace();
        selectedSeason = Seasons.First();

        LoadConstructorsCommand = new AsyncRelayCommand(GetConstructorsAsync);
    }

    [ObservableProperty]
    private int selectedSeason;
    public IReadOnlyList<int> Seasons => GetSeasons();


    public IAsyncRelayCommand LoadConstructorsCommand { get; }

    async Task GetConstructorsAsync()
    {
        var summary = await _ergastService.RaceSummaryAsync(2023, 1);

        var series = _raceTrace.CreateTraces(summary.DriverLapTimes);
        Debug.WriteLine(series.Count);

        var data = series.SelectMany(p => p.DataPoint.Select(dp => new { p.Driver, dp.Lap, dp.Time }));

        var drs = data.GroupBy(p => p.Driver);
        var tl = drs.Select(g => new LineSeries
        {
            Title = $"{g.Key.PermanentNumber:D2} {g.Key.Code}",
            ItemsSource = g.OrderBy(p => p.Lap).Select(p => new DataPoint(p.Lap, p.Time)),
            TrackerFormatString = "{0} \nLap: {2:0} Time: {4:F3}",
            CanTrackerInterpolatePoints = false,
        });

        InitialisePlotModel();

        foreach (var item in tl)
        {
            PlotModel.Series.Add(item);
        }

        OnPropertyChanged(nameof(PlotModel));
    }

    void InitialisePlotModel()
    {
        PlotModel = new();

        PlotModel.Legends.Add(new Legend
        {
            LegendPosition = LegendPosition.RightTop,
            LegendPlacement = LegendPlacement.Outside,
        });

        PlotModel.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Bottom,
            MajorStep = 5,
            MinorStep = 1,
            Title = "Laps",
        });

        PlotModel.Annotations.Add(new LineAnnotation
        {
            Type = LineAnnotationType.Horizontal,
            Y = 0,
            Color = OxyColors.Black,
        });
    }

    static IReadOnlyList<int> GetSeasons()
    {
        int startYear = 1996; //Ergast started traking lap times in 1996
        return Enumerable.Range(startYear, DateTime.Now.Year - startYear)
            .OrderByDescending(p => p).ToList();
    }

    public PlotModel PlotModel { get; private set; }

    readonly IErgastService _ergastService;
    readonly RaceTrace _raceTrace;
}
