using WPF_MVVM.Models;

namespace WPF_MVVM.Services.Interfaces;

internal interface IDataService
{
    IEnumerable<CountryInfo> GetData();
}