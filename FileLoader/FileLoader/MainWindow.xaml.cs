using Bll;
using Domain.Model;
using FileLoader.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FileLoader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool ManualResetEventChecked { get; set; }
        public bool FirstLoadChecked { get; set; }
        public bool AbortChecked { get; set; }
        public List<Thread> Threads { get; set; }

        private readonly LoaderFileService loaderFileService;
        public Semaphore pool { get; set; }
        private readonly ManualResetEvent manualResetEvent;
        public ObservableCollection<LoaderFile> References { get; set; }
        public MainWindow(LoaderFileService _loaderFileService)
        {
            ManualResetEventChecked = false;
            FirstLoadChecked = false;
            loaderFileService = _loaderFileService;
            manualResetEvent = new ManualResetEvent(false);
            References = new ObservableCollection<LoaderFile>(loaderFileService.GetFromCondition(x => x.Id > 0 && x.Status==false));
            Threads = new List<Thread>();
            InitializeComponent();
            this.Show();
            refListView.ItemsSource = References;

        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            PauseButton.IsEnabled = true;
            StopButton.IsEnabled = true;
            StartButton.IsEnabled = false;
            SemaforSlider.IsEnabled = false;
            refListView.IsEnabled = false;

            if (!FirstLoadChecked)
            {
                pool = new Semaphore((int)SemaforSlider.Value, (int)SemaforSlider.Maximum);
                foreach (var refer in References)
                {
                    var th = new Thread(Download);
                    Threads.Add(th);
                    th.IsBackground = true;
                    th.Start(refer);
                    
                }
                FirstLoadChecked = true;
            }

            ManualResetEventChecked = true;
            //manualResetEvent.Set();
        }
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = true;
            PauseButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            ManualResetEventChecked = false;
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            //foreach (var th in Threads)
            //{
            //    if (th.IsAlive)
            //    {
            //        th.Abort();
            //    }
            //}

            //SemaforSlider.IsEnabled = true;
            //refListView.IsEnabled = true;
            //References = new ObservableCollection<LoaderFile>(loaderFileService.GetFromCondition(x => x.Id > 0));
            refListView.ItemsSource = References;
            PauseButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            StartButton.IsEnabled = true;
            FirstLoadChecked = false;
        }

        public void Download(object obj)
        {
            if (obj is LoaderFile refer)
            {
                pool.WaitOne();
                using (var client = new WebClient())
                {
                    MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString() + "started");
                    string fullPath = Directory.GetCurrentDirectory() + refer.Reference.FileName();
                    client.DownloadFile(refer.Reference, fullPath);
                    var newRef = refer;
                    newRef.Status = true;
                    loaderFileService.Update(refer.Id, newRef);
                    DownloadProgressBar.Dispatcher.Invoke(ChaingeProgress);
                }
                if (ManualResetEventChecked == false)
                {
                    MessageBox.Show("false");
                    manualResetEvent.WaitOne();
                }

                pool.Release();

            }

        }

        private void AddRefButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReferenceTextBox.Text != null)
            {
                loaderFileService.Create(new LoaderFile { Reference = ReferenceTextBox.Text });
                References = new ObservableCollection<LoaderFile>(loaderFileService.GetFromCondition(x => x.Id > 0 && x.Status == false));
                refListView.ItemsSource = References;
                ReferenceTextBox.Text = string.Empty;
            }

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int id;
                int.TryParse(button.Tag.ToString(), out id);
                loaderFileService.Delete(id);
                References = new ObservableCollection<LoaderFile>(loaderFileService.GetFromCondition(x => x.Id > 0 && x.Status == false));
                refListView.ItemsSource = References;

            }
        }
        public void ChaingeProgress()
        {
            float delta = (float)100 / (float)References.Count;
            DownloadProgressBar.Value += delta;
            if (DownloadProgressBar.Value + delta / 2 >= DownloadProgressBar.Maximum)
            {
                MessageBox.Show("All Files download");
            }
        }

    }
}
