using System.Diagnostics;
using System.IO;
using WpfApp.ViewModels.Base;

namespace WpfApp.ViewModels
{
    internal class DirectoryViewModel : ViewModel
    {
        private readonly DirectoryInfo _directoryInfo;

        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    var directories = _directoryInfo
                        .EnumerateDirectories()
                        .Select(dir_info => new DirectoryViewModel(dir_info.FullName));
                    return directories;
                }
                catch (UnauthorizedAccessException ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
                return Enumerable.Empty<DirectoryViewModel>();
            }
        }

        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _directoryInfo
                        .EnumerateFiles()
                        .Select(file => new FileViewModel(file.FullName));
                    return files;
                }
                catch (UnauthorizedAccessException ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
                return Enumerable.Empty<FileViewModel>();   
            }
        }

        public IEnumerable<object> DirectoryItems => SubDirectories.Cast<object>().Concat(Files.Cast<object>());

        public string Name => _directoryInfo.Name;

        public string Path => _directoryInfo.FullName;

        public DateTime CreationTime => _directoryInfo.CreationTime;

        public DirectoryViewModel(string path)
        {
            _directoryInfo = new DirectoryInfo(path);
        }
    }
}
