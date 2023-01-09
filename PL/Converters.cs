using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Data;

namespace PL
{
    class ConvertImagePathToBitmap : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            try
            {
                string pictures = (string)value;
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + pictures;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
            }
            catch (Exception ex)
            {
                string pictures = @"\Pics\IMG.FAILS.jpg";
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + pictures;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
