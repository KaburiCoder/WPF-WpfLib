﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfLib.Converters
{
  public class InvertConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return -(double)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
