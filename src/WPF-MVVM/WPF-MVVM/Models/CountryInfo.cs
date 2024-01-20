using System.Text;
using System.Windows;

namespace WPF_MVVM.Models;

internal class CountryInfo : PlaceInfo
{
    private Point _location;

    public override Point Location
    {
        get
        {
            if(_location != default)
                return (Point)_location;
            if (ProvinceCounts is null) return default;
            var average_x = ProvinceCounts.Average(p => p.Location.X);
            var average_y = ProvinceCounts.Average(p => p.Location.Y);
            _location = new Point(average_x, average_y);
            return _location;
        }
        set => _location = value;
    }
    public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }

    private IEnumerable<ConfirmedCount> _counts;
    public override IEnumerable<ConfirmedCount> Counts
    {
        get
        {
            if (_counts != null) return _counts;
            var points_count = ProvinceCounts.FirstOrDefault()?.Counts.Count() ?? 0;
            if(points_count == 0) return Enumerable.Empty<ConfirmedCount>();

            var province_points = ProvinceCounts.Select(p => p.Counts.ToArray()).ToArray();

            var points = new ConfirmedCount[points_count];
            

            foreach (var province in province_points)
            {
                for (int i = 0; i < points_count; i++)
                {
                    if (points[i].Date == default) points[i] = province[i];
                    else points[i].Count += province[i].Count;
                }
            }

            return _counts = points;
        }
        set => _counts = value;
    }

    public override string ToString()
    {
        return new StringBuilder().Append($"{Name} ({Location.X},{Location.X}), CountPr = {ProvinceCounts.Count()}").ToString();
    }
}