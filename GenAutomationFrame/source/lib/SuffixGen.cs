using System;
using System.Collections.Generic;

namespace Plexure.Service.Security.IntegrationTest
{
    internal class SuffixGen
    {
        private static string _suffix;

        public static string Suffix
        {
            get
            {
                if (!String.IsNullOrEmpty(_suffix))
                    return _suffix;
                else
                {
                    GenerateTimeStamp();
                    return _suffix;
                }
            }
        }

        private static void GenerateTimeStamp()
        {
            _suffix = DateTime.Now.Ticks.ToString();
            _suffix = Suffix.Replace("/", "");
            _suffix = Suffix.Substring(0, Suffix.Length - 2);
        }

        public static void ReadRequestSuffix()
        {
            JsonReader reader = new JsonReader();
            var suffix = reader.ReadSingleEntry<Dictionary<string, string>>(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\Suffixes");
            if (String.IsNullOrEmpty(suffix["RequestSuffix"]))
                Environment.RequestSuffix = suffix["RequestSuffix"];
        }

        public static void WriteRequestSuffix()
        {
            JsonReader reader = new JsonReader();

            var suffix = reader.ReadSingleEntry<Dictionary<string, string>>(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\Suffixes");
            GenerateTimeStamp();
            suffix["RequestSuffix"] = _suffix;

            JsonWriter writer = new JsonWriter();
            writer.Write(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\Suffixes", suffix);
        }
    }
}