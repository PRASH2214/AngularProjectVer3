using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cubix.Models
{
     public class AppSetting
    {
        public static string ConnectionString { get; set; }
        public static string ConnectionType { get; set; }
        public static string Secret { get; set; }
        public static string ErrorLog { get; set; }
        public static string DocPath { get; set; }
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static string PaymentSecretKey { get; set; }
        public static string PaymentAPIKey { get; set; }

        public static string NotifyURL { get; set; }
        public static string ReturnURL { get; set; }
        public static string PaymentGatewayURL { get; set; }
        
        public static void SetSetting(IConfiguration config)
        {
            ConnectionString = config.GetSection("AppSettings:ConnectionString").Value;
            ConnectionType = config.GetSection("AppSettings:ConnectionType").Value;
            Secret = config.GetSection("AppSettings:Secret").Value;
            ErrorLog = config.GetSection("AppSettings:ErrorLog").Value;
            DocPath = config.GetSection("AppSettings:DocPath").Value;
            Issuer = config.GetSection("AppSettings:Issuer").Value;
            Audience = config.GetSection("AppSettings:Audience").Value;
            PaymentSecretKey = config.GetSection("AppSettings:PaymentSecretKey").Value;
            PaymentAPIKey = config.GetSection("AppSettings:PaymentAPIKey").Value;
            NotifyURL = config.GetSection("AppSettings:NotifyURL").Value;
            ReturnURL = config.GetSection("AppSettings:ReturnURL").Value;
            PaymentGatewayURL = config.GetSection("AppSettings:PaymentGatewayURL").Value;
        }
    }
}
