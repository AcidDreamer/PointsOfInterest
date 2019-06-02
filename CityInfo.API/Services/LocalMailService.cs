using CityInfo.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class LocalMailService : ILocalMailService
    {
        private string From = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string mailTo, string subject, string message)
        {
            Debug.WriteLine($"Mail from {From} to {mailTo} with LocalMailService");
            Debug.WriteLine($"Subject : {subject}");
            Debug.WriteLine($"Message : {message}");
        }
    }
}
