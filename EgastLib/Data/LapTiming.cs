/// <summary>
/// Driver lap time in seconds
/// </summary>
/// <param name="LapNumber">Lap number</param>
/// <param name="DriverId">Driver id, eg: alonso</param>
/// <param name="Position">Track position</param>
/// <param name="Time">Lap time in seconds</param>
public record LapTime(int LapNumber, string DriverId, int Position, double Time);