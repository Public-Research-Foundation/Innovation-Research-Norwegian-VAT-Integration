using Innovation_Research_Norwegian_VAT_Integration.Grensesnitt;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel.NorskSkatteBibliotek.Models.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Innovation_Research_Norwegian_VAT_Integration
{
    public class Main
    {
        private readonly INorskSkatteTjeneste _skatteTjeneste;
        private readonly IEnkeltPersonForetak _enkTjeneste;

        public Main(INorskSkatteTjeneste skatteTjeneste, IEnkeltPersonForetak enkTjeneste)
        {
            _skatteTjeneste = skatteTjeneste;
            _enkTjeneste = enkTjeneste;
        }

        public async Task BeregnSkattAsync()
        {
            var forespørsel = new SkatteBeregningsForesporsel
            {
                Inntekt = 500000m,
                Kommunenummer = "0301",
                Alder = 35
            };

            var resultat = await _skatteTjeneste.BeregnSkattAsync(forespørsel);

            // Bruk resultatet...
        }
    }

}
