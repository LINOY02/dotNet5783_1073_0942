using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Automation.Provider;

namespace PL
{
    class ConvertImagePathToBitmap : IValueConverter //Matching images to the product
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
    class ConvertBooleanToText : IValueConverter //According to the quantity of the product, we will update the catalog if it is in stock or not
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return " ";
            }
            else
            {
                return "Out Of Stock";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConvertBooleanToColors : IValueConverter //If the product isn't in stock, we will color the background of the product in the catalog in gray
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return Brushes.WhiteSmoke;
            }
            else
            {
                return Brushes.LightGray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class NotBooleanToVisibilityConverter : IValueConverter //If the product isn't in stock, you will not be given the option of collecting a product into a shopping basket
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConvertTextToReadOnly : IValueConverter //When we are in product update mode, the ID cannot be changed
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConvertBooleanToContent : IValueConverter //If we received an ID, that means we are in a product update mode and if the ID is empty, we are in the mode of adding a new product
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.ToString() != "")
            {
                return "Update";
            }
            else
            {
                return "Add";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConvertTextToBoolean : IMultiValueConverter //As long as one of the product details is empty, it will not be possible to update or add a new product
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            
            while (value[0].ToString() == "" || value[1].ToString() == "" || value[2].ToString() == "" || value[3].ToString() == "")
            {
                return false;
            }
            return true;
            
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConvertTextUserToBoolean : IMultiValueConverter //As long as one of the details is empty, it will not be possible to sign in
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {

            while (value[0].ToString() == "" || value[1].ToString() == "" || value[2].ToString() == "" || value[3].ToString() == "" || value[4].ToString() == "")
            {
                return false;
            }
            return true;

        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ConvertTextUserLogInToBoolean : IMultiValueConverter //As long as one of the details is empty, it will not be possible to log in
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {

            while (value[0].ToString() == "" || value[1].ToString() == "")
            {
                return false;
            }
            return true;

        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}