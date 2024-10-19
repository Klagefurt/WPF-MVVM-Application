using System.IO;
using WpfApp.ViewModels.Base;

namespace WpfApp.ViewModels
{
    internal class FileViewModel : ViewModel
    {
        private readonly FileInfo _fileInfo;

        public string Name => _fileInfo.Name;

        public string Path => _fileInfo.FullName;

        public DateTime CreationTime => _fileInfo.CreationTime;

        public FileViewModel(string path)
        {
            _fileInfo = new FileInfo(path);
        }
    }
}
