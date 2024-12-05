using System.Windows;

namespace WpfApp.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }
        public Point Location { get; set; }
        public IEnumerable<InfectedCount> InfectedCounts { get; set; }

    }
}
