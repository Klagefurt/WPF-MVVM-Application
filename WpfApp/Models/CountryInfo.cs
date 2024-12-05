namespace WpfApp.Models
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }
    }
}
