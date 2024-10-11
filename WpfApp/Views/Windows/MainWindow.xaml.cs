using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp.Models.University;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int tabControlCount;
        public MainWindow()
        {
            InitializeComponent();
            tabControlCount = MyTabControl.Items.Count;
        }

        public int GetTabControlCount() => tabControlCount;

        private void GroupsCollection_Filter(object sender, FilterEventArgs e)
        {
            if (e.Item is not Group group) return;
            if (group.Name is null) return;

            var filter_text = GroupNameFilterText.Text;
            if (filter_text.Length == 0) return;

            if (group.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (group.Description is not null && group.Description.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }

        private void GroupNameFilterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text_box = (TextBox)sender;
            var collection = text_box.FindResource("GroupsCollection") as CollectionViewSource;
            collection?.View.Refresh();
        }
    }
}