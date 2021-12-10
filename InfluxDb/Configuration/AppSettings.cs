﻿namespace InfluxDb.Configuration
{
    public class InfluxDbOptions
    {
        public const string InfluxDb = "InfluxDb";
        public string Host { get;  set; }
        public string Token { get;  set; }
        public string Bucket { get;  set; }
        public string Organization { get;  set; }
    }
}
