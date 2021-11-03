using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WebApp_Mappe2.Models;
using WebApp_Mappe2.DAL;
using Moq;

namespace xUnitTesting
{
    public class OrdreTest
    {
        [Fact]
        public async Task LagreOrdre()
        {
            /*----------Arrange---------*/
            Billett enBillett = new Billett
            {
                RuteNr = 1,
                AvgangNr = 1,
                RefPers = "Ola Nordmann",
                AntallBarn = 1,
                AntallVoksen = 2
            };
            var mock = new Mock<IOrdreRepository>();

        }
        public async Task EndreOrdre()
        {
            /*----------Arrange---------*/

        }
        public async Task SlettOrdre()
        {
            /*----------Arrange---------*/

        }
        public async Task HentOrdre()
        {
            /*----------Arrange---------*/

        }
        public async Task HentEnOrdre()
        {
            /*----------Arrange---------*/

        }
    }
}
