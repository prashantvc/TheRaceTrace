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
    }
    public PlotModel PlotModel { get; private set; }

    [ObservableProperty]
    int selectedSeason;
    public IReadOnlyList<int> Seasons => GetSeasons();

    [ObservableProperty]
    Race selectedRace;

    [ObservableProperty]
    IEnumerable<Race> raceList;

    [RelayCommand]
    async Task LoadRaceList()
    {
        var rl = await _ergastService.RaceListAsync(SelectedSeason);
        RaceList = rl.OrderByDescending(p => p.Round);
        OnPropertyChanged(nameof(RaceList));

        SelectedRace = RaceList.First();
    }

    [RelayCommand]
    async Task LoadRaceData()
    {
        var summary = await _ergastService.RaceSummaryAsync(SelectedSeason, SelectedRace.Round);

        var series = _raceTrace.CreateTraces(summary.DriverLapTimes);
        Debug.WriteLine(series.Count);

        var data = series.SelectMany(p => p.DataPoint.Select(dp => new { p.Driver, dp.Lap, dp.Time }));

        var drs = data.GroupBy(p => p.Driver);
        var tl = drs.Select(g => new LineSeries
        {
            Title = DriverDisplayTitle(g.Key),
            ItemsSource = g.OrderBy(p => p.Lap).Select(p => new DataPoint(p.Lap, p.Time)),
            TrackerFormatString = "{0}\nLap: {2:0}\nTime: {4:F3}",
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
        PlotModel = new()
        {
            Title = $"{SelectedRace.RaceName} ({SelectedSeason}) - {SelectedRace.Circuit.CircuitName}",
        };

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

    static string DriverDisplayTitle(Driver driver)
    {
        string pn = driver.PermanentNumber == 0 ? string.Empty : $"{driver.PermanentNumber:D2}";
        string name = driver.Code ?? driver.FamilyName;

        return $"{pn} {name}";
    }

    static IReadOnlyList<int> GetSeasons()
    {
        int startYear = 1996; //Ergast started traking lap times in 1996
        return Enumerable.Range(startYear, DateTime.Now.Year - startYear)
            .OrderByDescending(p => p).ToList();
    }
    readonly IErgastService _ergastService;
    readonly RaceTrace _raceTrace;
}
