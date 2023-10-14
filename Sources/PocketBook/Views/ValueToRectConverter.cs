using System;
using System.Globalization;

namespace PocketBook.Views
{
	public class ValueToRectConverter: IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float valueFloat = (float)value - 8;
            return new Rect(0, 0, valueFloat, 90);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

