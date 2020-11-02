using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace LogiticsWebApp.Utilities
{
    public static class CommonExtensions
    {
        public static string ToFileSize(this long size)
        {
            if (size < 1024) { return (size).ToString("F0") + " bytes"; }
            if (size < Math.Pow(1024, 2)) { return (size / 1024).ToString("F0") + "KB"; }
            if (size < Math.Pow(1024, 3)) { return (size / Math.Pow(1024, 2)).ToString("F0") + "MB"; }
            if (size < Math.Pow(1024, 4)) { return (size / Math.Pow(1024, 3)).ToString("F0") + "GB"; }
            if (size < Math.Pow(1024, 5)) { return (size / Math.Pow(1024, 4)).ToString("F0") + "TB"; }
            if (size < Math.Pow(1024, 6)) { return (size / Math.Pow(1024, 5)).ToString("F0") + "PB"; }
            return (size / Math.Pow(1024, 6)).ToString("F0") + "EB";
        }

        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
        public static XmlDocument ToXmlDocument(this XElement xElement)
        {
            var sb = new StringBuilder();
            var xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false };
            using (var xw = XmlWriter.Create(sb, xws))
            {
                xElement.WriteTo(xw);
            }
            var doc = new XmlDocument();
            doc.LoadXml(sb.ToString());
            return doc;
        }
        public static Stream ToMemoryStream(this XmlDocument doc)
        {
            var xmlStream = new MemoryStream();
            doc.Save(xmlStream);
            xmlStream.Flush();//Adjust this if you want read your data 
            xmlStream.Position = 0;
            return xmlStream;
        }


        public static string RemoveLastCharacter(this String instr)
        {
            return instr.Substring(0, instr.Length - 1);
        }
        public static string RemoveLast(this String instr, int number)
        {
            return instr.Substring(0, instr.Length - number);
        }
        public static string RemoveFirstCharacter(this String instr)
        {
            return instr.Substring(1);
        }
        public static string RemoveFirst(this String instr, int number)
        {
            return instr.Substring(number);
        }
        public static bool Between(this DateTime dt, DateTime rangeBeg, DateTime rangeEnd)
        {
            return dt.Ticks >= rangeBeg.Ticks && dt.Ticks <= rangeEnd.Ticks;
        }
        public static int CalculateAge(this DateTime dateTime)
        {
            var age = DateTime.Now.Year - dateTime.Year;
            if (DateTime.Now < dateTime.AddYears(age))
                age--;
            return age;
        }

        public static string ToReadableTime(this DateTime value)
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - value.Ticks);
            double delta = ts.TotalSeconds;
            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 120)
            {
                return "a minute ago";
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 5400) // 90 * 60
            {
                return "an hour ago";
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return "yesterday";
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return ts.Days + " days ago";
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
        public static bool WorkingDay(this DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
        public static DateTime NextWorkday(this DateTime date)
        {
            var nextDay = date;
            while (!nextDay.WorkingDay())
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }

        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - current.DayOfWeek;
            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }
            DateTime result = current.AddDays(offsetDays);
            return result;
        }

        public static Stream ToStream(this string str)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(str);
            //byte[] byteArray = Encoding.ASCII.GetBytes(str);
            return new MemoryStream(byteArray);
        }
        public static string ToString(this Stream stream)
        {
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static void CopyTo(this Stream fromStream, Stream toStream)
        {
            if (fromStream == null)
                throw new ArgumentNullException("fromStream");
            if (toStream == null)
                throw new ArgumentNullException("toStream");
            var bytes = new byte[8092];
            int dataRead;
            while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0)
                toStream.Write(bytes, 0, dataRead);
        }

        public static string OrdinalSuffix(this DateTime datetime)
        {
            int day = datetime.Day;
            if (day % 100 >= 11 && day % 100 <= 13)
                return String.Concat(day, "th");
            switch (day % 10)
            {
                case 1:
                    return String.Concat(day, "st");
                case 2:
                    return String.Concat(day, "nd");
                case 3:
                    return String.Concat(day, "rd");
                default:
                    return String.Concat(day, "th");
            }
        }

        public static int ToInt(this String input)
        {
            int result;
            if (!int.TryParse(input, out result))
            {
                result = 0;
            }
            return result;
        }
        public static int Occurrence(this String instr, string search)
        {
            return Regex.Matches(instr, search).Count;
        }
        public static string Join<T>(this IEnumerable<T> self, string separator)
        {
            return String.Join(separator, self.Select(e => e.ToString()).ToArray());
        }
        public static string Join(this Array array, string separator)
        {
            return String.Join(separator, array);
        }
        public static string ToQueryString(this NameValueCollection nvc)
        {
            return string.Join("&",
                nvc.AllKeys.Select(
                    key => string.Format("{0}={1}",
                        HttpUtility.UrlEncode(key),
                        HttpUtility.UrlEncode(nvc[key]))));
        }
        public static NameValueCollection ToList<T>() where T : struct
        {
            var result = new NameValueCollection();
            if (!typeof(T).IsEnum) return result;
            var enumType = typeof(T);
            var values = Enum.GetValues(enumType);
            foreach (var value in values)
            {
                var memInfo = enumType.GetMember(enumType.GetEnumName(value));
                var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = descriptionAttributes.Length > 0
                    ? ((DescriptionAttribute)descriptionAttributes.First()).Description
                    : value.ToString();
                result.Add(description, value.ToString());
            }
            return result;
        }


        public static string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";
            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";


            char[] sep = {
            ','
        };
            string[] arr = allowedChars.Split(sep);


            string IDString = "";
            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }

            if (!NewPassword.Any(x => char.IsDigit(x)))
            {
                Random rand1 = new Random();
                char[] pass = NewPassword.ToCharArray();
                pass[rand.Next(NewPassword.Length)] = Convert.ToChar(rand.Next(10) + '0');
                NewPassword = new string(pass);
            }
            return NewPassword;
        }

        public static int GetDigit(string a)
        {

            string b = string.Empty;
            int val = 0;

            for (int i = 0; i < a.Length; i++)
            {
                if (Char.IsDigit(a[i]))
                    b += a[i];
            }

            if (b.Length > 0)
                val = int.Parse(b);

            return val;
        }

        public static string ConvertToCamelCase(String str)
        {
            // Create a char array of 
            // given String 
            char[] ch = str.ToCharArray();

            for (int i = 0; i < str.Length; i++)
            {
                // If first character of a 
                // word is found 
                if (i == 0 && ch[i] != ' ' || ch[i] != ' ' && ch[i - 1] == ' ')
                {
                    // If it is in lower-case 
                    if (ch[i] >= 'a' && ch[i] <= 'z')
                    {
                        // Convert into Upper-case 
                        ch[i] = (char)(ch[i] - 'a' + 'A');
                    }
                }

                // If apart from first character 
                // Any one is in Upper-case 
                else if (ch[i] >= 'A' && ch[i] <= 'Z')
                {
                    // Convert into Lower-Case 
                    ch[i] = (char)(ch[i] + 'a' - 'A');
                }

            }

            // Convert the char array to 
            // equivalent String 
            String st = new String(ch);

            return st;
        }

        public static string ConvertToString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(obj);
            }
        }

        public static string RoundTo8(string Value)
        {
            var res = "0";
            if (!string.IsNullOrEmpty(Value))
            {
                var amount = Convert.ToDecimal(Value);
                var integeramount = Convert.ToInt32(amount * 100);
                res = Convert.ToString(integeramount).PadLeft(8, '0');
            };

            return res;
        }

        public static int CalculateAge(string date)
        {
            DateTime birthdate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - birthdate.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthdate > today.AddYears(-age)) age--;
            return age;
        }


    }
}


