using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryWatcher
{
    public class Watcher
    {
        public event FileSystemEventHandler ContentChanged;
        public event FileSystemEventHandler ContentRenamed;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Run(string dir)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = dir;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;

            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            ContentChanged?.Invoke(source, e);
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            ContentRenamed?.Invoke(source, e);
        }
    }
}
