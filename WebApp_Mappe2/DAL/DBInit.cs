﻿using Microsoft.AspNetCore.Builder;
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
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var dest1 = new Destinasjoner { Land = "Norge", Sted = "Oslo" };
                var dest2 = new Destinasjoner { Land = "Danmark", Sted = "Fredrikshavn" };

                var rute1 = new Ruter { FraDestinasjon = dest1, TilDestinasjon = dest2, PrisBarn = 99, PrisVoksen = 199 };
                var rute2 = new Ruter { FraDestinasjon = dest2, TilDestinasjon = dest1, PrisBarn = 99, PrisVoksen = 199 };

                var avgang1 = new Avganger { RuteNr = rute1, AvgangTid = new DateTime(2021, 10, 10, 19, 00, 00) }; //"01/09/2023 12:12:00" };
                var avgang2 = new Avganger { RuteNr = rute2, AvgangTid = new DateTime(2021, 11, 10, 18, 30, 00) };  //"11/01/2023 12:12:00"};
                var avgang3 = new Avganger { RuteNr = rute2, AvgangTid = new DateTime(2021, 11, 12, 18, 30, 00) }; //"12/02/2023 12:12:00" };
                var ordre1 = new Ordrer { RuteNr = rute1, AvgangNr = avgang1, AntallBarn = 1, AntallVoksen = 1, RefPers = "test" };
                context.Destinasjoner.Add(dest1);
                context.Destinasjoner.Add(dest2);
                context.Ruter.Add(rute1);
                context.Ruter.Add(rute2);
                context.Avganger.Add(avgang1);
                context.Avganger.Add(avgang2);
                context.Avganger.Add(avgang3);
                context.Ordrer.Add(ordre1);

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
