using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MoviesRepositoryMVC
{
    public static class SettingsReaderClass
    {
        public static string UrlString
        {
            get { return Setting("HostURI"); }
        }

        private static string Setting(string name)
        {
            string value = ConfigurationManager.AppSettings[name];
            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationErrorsException("The configuration of the Hot URI is not done.");
            }
            return value;
        }
    }
}