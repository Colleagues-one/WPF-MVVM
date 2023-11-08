namespace WPF_MVVM.Models;

internal class CountryInfo : PlaceInfo
{
    public IEnumerable<ProvinceInfo> ProvinceCounts { get; set; }
}