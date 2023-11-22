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

    public override string ToString()
    {
        return new StringBuilder().Append($"{Name} ({Location.X},{Location.X}), CountPr = {ProvinceCounts.Count()}").ToString();
    }
}