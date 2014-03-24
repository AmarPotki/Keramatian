using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using StructureMap;
using Keramatian.Common;
using System.Globalization;
using Keramatian.Services;

namespace Keramatian.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex EmailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex WebUrlExpression = new Regex(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex StripHtmlExpression = new Regex("<\\S[^><]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex BlockNameExpression = new Regex(@"name:([a-zA-Z0-9 ])+", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        [DebuggerStepThrough]
        public static string FormatWith(this string instance, params object[] args)
        {
            Check.Argument.IsNotNullOrEmpty(instance, "instance");

            return string.Format(CultureInfo.CurrentUICulture, instance, args);
        }

        [DebuggerStepThrough]
        public static string Hash(this string instance)
        {
            Check.Argument.IsNotNullOrEmpty(instance, "instance");

            using (MD5 md5 = MD5.Create())
            {
                byte[] data = Encoding.Unicode.GetBytes(instance);
                byte[] hash = md5.ComputeHash(data);

                return Convert.ToBase64String(hash);
            }
        }

        [DebuggerStepThrough]
        public static T ToEnum<T>(this string instance, T defaultValue) where T : struct, IComparable, IFormattable
        {
            T convertedValue = defaultValue;

            if (!string.IsNullOrWhiteSpace(instance) && !Enum.TryParse(instance.Trim(), true, out convertedValue))
            {
                convertedValue = defaultValue;
            }

            return convertedValue;
        }

        [DebuggerStepThrough]
        public static string StripHtml(this string instance)
        {
            Check.Argument.IsNotNullOrEmpty(instance, "instance");

            return StripHtmlExpression.Replace(instance, string.Empty);
        }

        [DebuggerStepThrough]
        public static bool IsEmail(this string instance)
        {
            return !string.IsNullOrWhiteSpace(instance) && EmailExpression.IsMatch(instance);
        }

        [DebuggerStepThrough]
        public static bool IsWebUrl(this string instance)
        {
            return !string.IsNullOrWhiteSpace(instance) && WebUrlExpression.IsMatch(instance);
        }

        [DebuggerStepThrough]
        public static bool IsIPAddress(this string instance)
        {
            IPAddress ip;

            return !string.IsNullOrWhiteSpace(instance) && IPAddress.TryParse(instance, out ip);
        }
        [DebuggerStepThrough]
        public static string ToSlug(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return string.Empty;
            value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
            value = value.Replace("-", "");
            value = value.ToAlphaNumeric().ToSingleSpace(); // remove invalid chars
            //value = value.Replace(" ", ""); // remove all spaces
            value = value.Replace(" ", "-"); // replace spaces
            value = value.Substring(0, value.Length <= 170 ? value.Length : 170).Trim(); // cut and trim it

            return value.ToLower();
        }
        [DebuggerStepThrough]
        public static string ToAlphaNumeric(this string value)
        {
            return Regex.Replace(value, @"[^a-zA-Z0-9\s-]", "");
        }
        [DebuggerStepThrough]
        public static string ToSingleSpace(this string value)
        {
            return Regex.Replace(value, @"\s+", " ").Trim();
        }

        [DebuggerStepThrough]
        public static string ToHumanFromPascal(this string s)
        {
            if (2 > s.Length)
            {
                return s;
            }
            var sb = new StringBuilder();
            var ca = s.ToCharArray();
            sb.Append(ca[0]);
            for (var i = 1; i < ca.Length - 1; i++)
            {
                var c = ca[i];
                if (char.IsUpper(c) && (char.IsLower(ca[i + 1]) || char.IsLower(ca[i - 1])))
                {
                    sb.Append(' ');
                }
                sb.Append(c);
            }
            sb.Append(ca[ca.Length - 1]);
            return sb.ToString();
        }

        public static string ToHumanFromSlug(this string s)
        {
            if (2 > s.Length)
            {
                return s;
            }
            var sb = new StringBuilder();
            var ca = s.ToCharArray();
            for (var i = 0; i < ca.Length; i++)
            {
                var c = ca[i];
                if (i == 0)
                    c = char.ToUpper(c);
                if (char.IsLower(c) && i > 0 && ca[i - 1] == '-' && c != 'a')
                {
                    c = char.ToUpper(c);
                }
                sb.Append(c);
            }
            return sb.ToString().Replace("-", " ");
        }

        public static string Nl2Br(this string value)
        {
            return value.Replace("\n", "<br />").Trim();
        }

        public static string Truncate(this string value)
        {
            if (value.Length > 30)
            {
                value = value.Substring(0, 30);
                var lastSpace = value.LastIndexOf(" ");
                if (lastSpace > 0)
                    value = value.Substring(0, lastSpace);
                value = value + "...";
            }
            return value;
        }

        public static string ToHash(this string value, string salt)
        {
            var cryptographer = ObjectFactory.GetInstance<ICryptographer>();
            return cryptographer.GetHashFromKeyAndSalt(value, salt);
        }

        public static string Append(this string target, string value)
        {
            return target + value;
        }

        public static string GetBlockName(this string str)
        {
            return BlockNameExpression.Match(str).ToString().Split(':')[1];
        }
    }

}