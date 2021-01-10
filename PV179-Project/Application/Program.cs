using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var contextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseNpgsql(@"Host=localhost;Database=tripdb;Username=postgres;Password=postgres;Port=5432")
                .Options;
            using (var ctx = new DatabaseContext(contextOptions))
            {
                var user = ctx.Users.Include(c => c.Challenges).First(u => u.Id == 18);
                //var user = ctx.Users.First(u => u.Id == 18);
                //var userC = ctx.Users.FirstOrDefault(u => u.Id == 18)?.Challenges.ToList();
                /*
                var c = new Challenge
                {
                    User = user,
                    Type = ChallengeType.Monthly,
                    TripCount = 3
                };
                ctx.Challenges.Add(c);
                ctx.SaveChanges();*/
                //var chall = ctx.Challenges.Where(c => c.UserId == 18).ToList();
                //Console.WriteLine(chall.Count);c
               // Console.WriteLine(userC.Count);
                if (user == null)
                {
                    Console.WriteLine("oopise");
                }

                if (user.Challenges == null)
                {
                    Console.WriteLine("nullboi");
                }
                Console.WriteLine(user.Challenges.Count);
            }
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
