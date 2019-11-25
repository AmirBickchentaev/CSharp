
using MoyapopitkaNomerPYAT.Directories;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
namespace MoyapopitkaNomerPYAT
{

    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImage : IValueConverter
    {
        public static HeaderToImage Instance = new HeaderToImage();

     
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;
            if (path == null)
            {
                return null;

            }
            var name = DirectoryStructure.getFileFolderName(path);
            var image = "images/box.png";
            if (string.IsNullOrEmpty(name))
            {
                image = "images/drive.jpg";
            }
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
            {
                image = "images/sdafs.jpg";
            }
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
