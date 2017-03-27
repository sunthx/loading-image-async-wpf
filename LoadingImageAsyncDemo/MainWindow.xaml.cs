using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace LoadingImageAsyncDemo
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Images = new ObservableCollection<ImageItem>();
            Images1 = new ObservableCollection<ImageItem>();
            Images2 = new ObservableCollection<ImageItem>();

            DataContext = this;
        }

        public ObservableCollection<ImageItem> Images { get; set; }
        public ObservableCollection<ImageItem> Images1 { get; set; }
        public ObservableCollection<ImageItem> Images2 { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Directory.GetFiles("test").ToList().ForEach(item => { Images.Add(new ImageItem {FilePath = item}); });

            Directory.GetFiles("test1").ToList().ForEach(item => { Images1.Add(new ImageItem {FilePath = item}); });

            Directory.GetFiles("test2").ToList().ForEach(item => { Images2.Add(new ImageItem {FilePath = item}); });
        }
    }
}