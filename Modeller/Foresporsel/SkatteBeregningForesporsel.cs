using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel
{
    using System.Collections.Generic;

    namespace NorskSkatteBibliotek.Models.Requests
    {
        /// <summary>
        /// Forespørsel for beregning av personlig skatt
        /// </summary>
        public class SkatteBeregningsForesporsel
        {
            /// <summary>
            /// Personenets totale inntekt for skatteåret
            /// </summary>
            public decimal Inntekt { get; set; }

            /// <summary>
            /// Kommunenummer for beregning av kommunal skatt
            /// </summary>
            public string Kommunenummer { get; set; }

            /// <summary>
            /// Om personen er pensjonist
            /// </summary>
            public bool ErPensjonist { get; set; }

            /// <summary>
            /// Alderen til personen
            /// </summary>
            public int Alder { get; set; }

            /// <summary>
            /// Tilleggsparametre for fleksibilitet i beregninger
            /// </summary>
            public Dictionary<string, object> Tilleggsparametre { get; set; } = new Dictionary<string, object>();
        }
    }
}
