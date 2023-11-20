namespace WPF_MVVM.Models;

internal class CountryInfo : PlaceInfo
{
    public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }
}