using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace LoadingImageAsyncDemo
{
    public class BitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string filePath = value.ToString();
            var task = Task.Factory.StartNew(() => CreateBitmapImage(filePath));
            return new TaskCompletionNotifier<BitmapImage>(task);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static BitmapImage CreateBitmapImage(string imagePath)
        {
            var bitmap = new BitmapImage();
            FileStream fileStream;
            using (fileStream = new FileStream(imagePath, FileMode.Open))
            {
                BinaryReader binReader;
                using (binReader = new BinaryReader(fileStream))
                {
                    var fileInfo = new FileInfo(imagePath);
                    var bytes = binReader.ReadBytes((int) fileInfo.Length);
                    binReader.Close();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(bytes);
                    bitmap.EndInit();
                    bitmap.Freeze();
                }

                fileStream.Close();
            }

            return bitmap;
        }
    }
}