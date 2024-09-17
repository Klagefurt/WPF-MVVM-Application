namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        int tabControlCount;
        public MainWindow()
        {
            InitializeComponent();
            tabControlCount = MyTabControl.Items.Count;
        }

        public int GetTabControlCount() => tabControlCount;
    }
}