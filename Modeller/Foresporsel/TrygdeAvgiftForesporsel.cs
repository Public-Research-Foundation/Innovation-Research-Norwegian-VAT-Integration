using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel
{
    namespace NorskSkatteBibliotek.Models.Requests
    {
        /// <summary>
        /// Forespørsel for beregning av trygdeavgift
        /// </summary>
        public class TrygdeAvgiftForespørsel
        {
            /// <summary>
            /// Personenets totale inntekt for beregning av trygdeavgift
            /// Trygdeavgift beregnes kun på inntekt mellom 69 900 NOK og 750 000 NOK (2024)
            /// </summary>
            public decimal Inntekt { get; set; }

            /// <summary>
            /// Angir om personen er selvstendig næringsdrivende
            /// Selvstendig næringsdrivende har høyere trygdeavgift (11.4% vs 8.2%)
            /// </summary>
            public bool ErSelvstendigNæringsdrivende { get; set; }
        }
    }
}
