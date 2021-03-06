﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Verdant
{
    public static class Tools
    {
        public static BitmapImage UrlToXamlImage(string url)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(url, UriKind.Absolute);
            bi.EndInit();
            return bi;
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InternetGetCookieEx(string pchURL, string pchCookieName, StringBuilder pchCookieData, ref uint pcchCookieData, int dwFlags, IntPtr lpReserved);
        private const int INTERNET_COOKIE_HTTPONLY = 0x00002000;
        public static string GetCookieStringData(string cookieUri)
        {
            uint datasize = 1024;
            StringBuilder cookieData = new StringBuilder((int)datasize);
            if (InternetGetCookieEx(cookieUri, null, cookieData, ref datasize, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero) && cookieData.Length > 0)
            {
                return cookieData.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
