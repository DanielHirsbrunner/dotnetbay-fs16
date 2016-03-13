using System;
using System.Globalization;
using System.Windows.Data;

namespace DotNetBay.WPF.Converter {
        public class IsClosedToStatusConverer : IValueConverter {
            public object Convert(object value, Type targetType,
                object parameter, CultureInfo culture) {
                return (bool)value ? "abgeschlossen" : "offen";
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
                throw new NotImplementedException();
            }
        }
    }
