using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Mappe2.DAL
{
    [ExcludeFromCodeCoverage]
    public class DBInit
    {
        public static void InitDB(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();
                context.Database.EnsureCreated();
              
                var bruker = new Brukere();
                bruker.Brukernavn = "admin";
                string passord = "admin";
                byte[] salt = BrukerRepository.LagSalt();
                byte[] hash = BrukerRepository.LagHash(passord, salt);
                bruker.Passord = hash;
                bruker.Salt = salt;
                context.Brukere.Add(bruker);

                context.SaveChanges();

            }           
        }
    }
}
