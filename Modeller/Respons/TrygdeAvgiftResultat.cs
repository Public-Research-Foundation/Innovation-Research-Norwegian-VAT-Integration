using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons
{
    /// <summary>
    /// Resultat fra beregning av trygdeavgift
    /// Inneholder detaljert informasjon om trygdeavgiftsberegning
    /// </summary>
    public class TrygdeAvgiftResultat
    {
        /// <summary>
        /// Opprinnelig inntekt som ble sendt inn for beregning
        /// </summary>
        public decimal Inntekt { get; set; }

        /// <summary>
        /// Beregnet trygdeavgift basert på inntekt og regler
        /// </summary>
        public decimal Trygdeavgift { get; set; }

        /// <summary>
        /// Justert grunnlag etter nedre og øvre grense
        /// Dette er beløpet trygdeavgift faktisk beregnes på
        /// </summary>
        public decimal BeregnetGrunnlag { get; set; }

        /// <summary>
        /// Sats som ble brukt for beregning (i prosent)
        /// 8.2% for arbeidstakere, 11.4% for selvstendig næringsdrivende
        /// </summary>
        public decimal Sats { get; set; }

        /// <summary>
        /// Nedre grense for trygdeavgift (69 900 NOK for 2024)
        /// Inntekt under denne betaler ikke trygdeavgift
        /// </summary>
        public decimal NedreGrense { get; set; }

        /// <summary>
        /// Øvre grense for trygdeavgift (750 000 NOK for 2024)
        /// Inntekt over denne betaler ikke trygdeavgift på overskytende beløp
        /// </summary>
        public decimal ØvreGrense { get; set; }

        /// <summary>
        /// Angir om beregningen var for selvstendig næringsdrivende
        /// </summary>
        public bool ErSelvstendigNæringsdrivende { get; set; }
    }
}
