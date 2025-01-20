using System.Windows;

namespace WpfApp.Models
{
    internal class CountryInfo : PlaceInfo
    {
        private Point? _Location;
        private IEnumerable<InfectedCount> _Counts;

        public override Point Location
        {  
            get 
            {
                if (_Location != null)
                    return (Point)_Location;

                if (ProvinceCounts == null)
                    return default;

                var average_x = ProvinceCounts.Average(p => p.Location.X);
                var average_y = ProvinceCounts.Average(p => p.Location.Y);

                return (Point)(_Location = new Point(average_x, average_y));
            }
            set => _Location = value;
        }

        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }

        public override IEnumerable<InfectedCount> InfectedCounts 
        {
            get
            {
                if (_Counts != null) return _Counts;

                var points_count = ProvinceCounts.FirstOrDefault()?.InfectedCounts?.Count() ?? 0;

                if (points_count == 0) return Enumerable.Empty<InfectedCount>();

                var province_points = ProvinceCounts.Select(p => p.InfectedCounts.ToArray()).ToArray();

                var points = new InfectedCount[points_count];
                foreach (var province in province_points)
                    for (var i = 0; i < points_count; i++)
                    {
                        if (points[i].Date == default)
                            points[i] = province[i];
                        else
                            points[i].Count += province[i].Count;
                    }

                return _Counts = points;
            }
            set => _Counts = value; 
        }
    }
}
