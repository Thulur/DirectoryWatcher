using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DirectoryWatcher
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok && dialog.FileName != "")
            {
                textBox.Text = dialog.FileName;

                var watcher = new Watcher();
                watcher.ContentChanged += WatcherContentChanged;
                watcher.ContentRenamed += WatcherContentRenamed;

                await Task.Run(() => { watcher.Run(dialog.FileName); });
            }
        }

        private void WatcherContentRenamed(object sender, System.IO.FileSystemEventArgs e)
        {
            Dispatcher.Invoke(() => { label.Content = "Name changed."; });
        }

        private void WatcherContentChanged(object sender, System.IO.FileSystemEventArgs e)
        {
            Dispatcher.Invoke(() => { label.Content = "Content changed."; });
        }
    }
}
