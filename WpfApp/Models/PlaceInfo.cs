using System.Windows;

namespace WpfApp.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }
        public virtual Point Location { get; set; }
        public virtual IEnumerable<InfectedCount> InfectedCounts { get; set; }

        public override string ToString()
        {
            return $"{Name}({Location})";
        }

    }
}
