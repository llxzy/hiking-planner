using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PV179_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new DatabaseContext())
            {
                
                var nTrip = new Trip();
                var nUser = new User()
                {
                    Name = "test",
                    MailAddress = "a"
                };
                db.Users.Add(nUser);
                nTrip.Author = nUser;
                db.SaveChanges();
                Console.WriteLine(db.Trips.Count());
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}