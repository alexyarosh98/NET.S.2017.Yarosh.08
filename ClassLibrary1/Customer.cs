using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerFormatter
{
    public class Customer : IFormattable
    {
        public string Name { get; set; }
        private long phone;
        public long Phone
        {
            get { return phone; }
            set
            {
                if (value.ToString().Length == 11)
                    phone = value;
                else throw new ArgumentException($"{nameof(value)} is not allowed to be a phone");
            }
        }
        public decimal Revanue { get; set; }
        /// <summary>
        /// Convert an object Customer to a string view
        /// </summary>
        /// <returns>string view of an object</returns>
        public override string ToString()
        {
            return this.ToString("G", new MFormatProvider());
        }

        /// <summary>
        /// Convert an object Customer to a string view
        /// </summary>
        /// <returns>string view of an object</returns>
        public string ToString(string format) => $"Customer record: {Name}, Phone: {Phone}, Revanue: {Revanue}";
        /// <summary>
        /// Convert an object Customer to a string view
        /// </summary>
        /// <param name="format">A format string containing formatting specifications</param>
        /// <param name="fp">An object that supplies format information about the current instance</param>
        /// <returns>The string representation of the value of arg, formatted as specified by format and formatProvider.</returns>
        public string ToString(string format, IFormatProvider fp)
        {
            if (string.IsNullOrEmpty(format)) format = "G";
            if (fp == null) fp = CultureInfo.CurrentCulture;
            string res = string.Empty;
            switch (format.Length > 1 ? format.Substring(0, 1) : format)
            {
                case "G":
                    res =this.ToString();
                    break;
                case "A":
                    res = $"Customer record: {Name}, Phone: {Phone}, Revanue: {Revanue}";
                    break;
                case "R":
                    res = string.Format("Customer record: Revanue: {0:C2}", Revanue);
                    break;
                default:
                    throw new FormatException($"The format of '{format}' is invalid.");
            }
            return res;
        }

    }
}
