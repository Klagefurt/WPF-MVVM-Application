namespace WpfApp.Models.University
{
    internal class Group
    {
        public string Name { get; set; }
        public IList<Student> Students { get; set; }
        public string Description { get; set; }
    }
}
