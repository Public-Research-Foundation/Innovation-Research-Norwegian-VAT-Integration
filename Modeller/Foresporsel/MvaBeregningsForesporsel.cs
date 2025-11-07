using Innovation_Research_Norwegian_VAT_Integration.Oppramsingstyper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel
{
  

    namespace NorskSkatteBibliotek.Models.Requests
    {
        /// <summary>
        /// Forespørsel for beregning av merverdiavgift (MVA)
        /// </summary>
        public class MvaBeregningsForesporsel
        {
            /// <summary>
            /// Beløpet som MVA skal beregnes for
            /// Avhengig av InkludererMva vil dette være brutto eller netto beløp
            /// </summary>
            public decimal Beløp { get; set; }

            /// <summary>
            /// Type MVA som skal beregnes
            /// Standard: 25% for de fleste varer og tjenester
            /// Redusert: 15% for f.eks. næringsmidler, persontransport
            /// Matvarer: 15% for matvarer
            /// </summary>
            public MvaType MvaType { get; set; }

            /// <summary>
            /// Angir om beløpet inkluderer MVA (true) eller er eksklusiv MVA (false)
            /// True: beløpet er inkludert MVA - MVA må trekkes ut
            /// False: beløpet er eksklusiv MVA - MVA må legges til
            /// </summary>
            public bool InkludererMva { get; set; }

            /// <summary>
            /// Beskrivelse av varen/tjenesten for dokumentasjon
            /// </summary>
            public string Beskrivelse { get; set; }
        }
    }
}
