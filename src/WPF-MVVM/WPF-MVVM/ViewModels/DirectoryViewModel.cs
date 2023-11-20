using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.ViewModels.Base;

namespace WPF_MVVM.ViewModels
{
    internal class DirectoryViewModel : BaseViewModel
    {
        private readonly DirectoryInfo _direcoryInfo;

        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    return _direcoryInfo
                              .EnumerateDirectories()
                              .Select((dir_info => new DirectoryViewModel(dir_info.FullName)));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                   
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
                    return _direcoryInfo
                    .EnumerateFiles()
                    .Select(file => new FileViewModel(file.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());

                }
                return Enumerable.Empty<FileViewModel>();
            }
        }

        public IEnumerable<object> DirectoryItems
        {
            get
            {
                try
                {
                    return SubDirectories.Cast<object>().Concat(Files);
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());

                }

                return Enumerable.Empty<object>();
            }
        }

        public string Name => _direcoryInfo.Name;

        public string Path => _direcoryInfo.FullName;

        public DateTime CreationTime => _direcoryInfo.CreationTime;


        public DirectoryViewModel(string Path)
        {
            _direcoryInfo = new DirectoryInfo(Path);
        }
    }


    class FileViewModel:BaseViewModel
    {
        private FileInfo _fileInfo;

        public string Name => _fileInfo.Name;

        public string Path => _fileInfo.FullName;

        public DateTime CreationTime => _fileInfo.CreationTime;

        public FileViewModel(string Path) => _fileInfo = new FileInfo(Path);
        
    }
}
