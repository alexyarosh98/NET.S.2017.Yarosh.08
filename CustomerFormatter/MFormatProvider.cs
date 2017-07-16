using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerFormatter
{
    public class MFormatProvider : IFormatProvider, ICustomFormatter
    {/// <summary>
     /// Returns an object that provides formatting services for the specified type.
     /// </summary>
     /// <param name="type">An object that specifies the type of format object to return.</param>
     /// <returns>An instance of the object specified by formatType, if the IFormatProvider implementation can supply that type of object; otherwise, null.</returns>
        public object GetFormat(Type type)
        {
            return type == typeof(ICustomFormatter) ? this : null;
        }

        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format">A format string containing formatting specifications.</param>
        /// <param name="obj">An object to format.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <returns>The string representation of the value of arg, formatted as specified by format and formatProvider.</returns>
        public string Format(string format, object obj, IFormatProvider formatProvider)
        {
            if (obj.GetType() != typeof(Customer) || string.IsNullOrEmpty(format))
            {
                return HandleOtherFormats(format, formatProvider);
            }

            string res = string.Empty;
            if (obj.GetType() == typeof(Customer) && !String.IsNullOrEmpty(format))
            {
                Customer customer = (Customer)obj;

                switch (format.ToUpper())
                {
                    case "FULLR":
                        res = string.Format("Customer record: Name: {0}, Phone: {1:+ # (###) ### ####}, Revanue: {2:C}", customer.Name, customer.Phone, customer.Revanue);
                        break;
                    case "FULLE":
                        res = string.Format("Customer record: Name: {0}, Phone: {1:+ # (###) ### ####}, Revanue: {2}", customer.Name, customer.Phone, (customer.Revanue / 2).ToString("C", new CultureInfo("en-US")));
                        break;
                    default:
                        try
                        {
                            return HandleOtherFormats(format, obj);
                        }
                        catch (FormatException)
                        {
                            throw new FormatException($"The format of '{format}' is invalid.");
                        }
                }
            }

            return res;
        }
        private static string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            else if (arg != null)
                return arg.ToString();
            else
                return String.Empty;
        }

    }
}
