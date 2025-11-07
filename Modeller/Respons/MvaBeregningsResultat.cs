using Innovation_Research_Norwegian_VAT_Integration.Oppramsingstyper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons
{
    /// <summary>
    /// Resultat fra beregning av merverdiavgift (MVA)
    /// Inneholder alle beløp for MVA-beregning i henhold til norske regler
    /// </summary>
    public class MvaBeregningsResultat
    {
        /// <summary>
        /// Netto beløp eksklusiv MVA
        /// Dette er beløpet uten mva hvis input var inklusive mva
        /// </summary>
        public decimal NettoBeløp { get; set; }

        /// <summary>
        /// Beregnet MVA-beløp
        /// Beløpet som skal betales i merverdiavgift
        /// </summary>
        public decimal MvaBeløp { get; set; }

        /// <summary>
        /// Brutto beløp inklusiv MVA
        /// Dette er beløpet inklusive mva hvis input var eksklusive mva
        /// </summary>
        public decimal BruttoBeløp { get; set; }

        /// <summary>
        /// MVA-sats som ble brukt for beregning (i prosent)
        /// 25% for standard, 15% for redusert/matvarer
        /// </summary>
        public decimal MvaSats { get; set; }

        /// <summary>
        /// Type MVA som ble brukt for beregning
        /// Standard, redusert eller matvarer
        /// </summary>
        public MvaType MvaType { get; set; }

        /// <summary>
        /// Angir om opprinnelig beløp inkluderte MVA
        /// </summary>
        public bool OpprinneligInkluderteMva { get; set; }

        /// <summary>
        /// Beskrivelse av varen/tjenesten
        /// </summary>
        public string Beskrivelse { get; set; }
    }
}
