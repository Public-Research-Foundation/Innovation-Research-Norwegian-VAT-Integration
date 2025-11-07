using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons
{
    /// <summary>
    /// Resultat fra beregning av personlig skatt
    /// Inneholder detaljert informasjon om skatteberegning og fordeling
    /// </summary>
    public class SkatteBeregningsResultat
    {
        /// <summary>
        /// Brutto inntekt før skatt (opprinnelig inntekt)
        /// Dette er beløpet som ble sendt inn for beregning
        /// </summary>
        public decimal BruttoInntekt { get; set; }

        /// <summary>
        /// Netto inntekt etter at all skatt er trukket
        /// Dette er beløpet personen faktisk har til disposisjon
        /// </summary>
        public decimal NettoInntekt { get; set; }

        /// <summary>
        /// Total skatt betalt inkludert alle skattetyper
        /// Inkluderer personskatt, trygdeavgift, eventuell fellesskatt
        /// </summary>
        public decimal TotalSkatt { get; set; }

        /// <summary>
        /// Effektiv skattesats i prosent
        /// Beregnes som (TotalSkatt / BruttoInntekt) * 100
        /// </summary>
        public decimal Skattesats { get; set; }

        /// <summary>
        /// Detaljert oppdeling av skatten i ulike kategorier
        /// Inneholder beløp for hver enkelt skattetype
        /// </summary>
        public Dictionary<string, decimal> SkattOppdeling { get; set; } = new Dictionary<string, decimal>();

        /// <summary>
        /// Angir om beregningen var vellykket
        /// False ved feil i input eller beregning
        /// </summary>
        public bool Suksess { get; set; }

        /// <summary>
        /// Beskrivende melding fra beregningen
        /// Kan inneholde feilmeldinger, advarsler eller informasjon
        /// </summary>
        public string Melding { get; set; }

        /// <summary>
        /// Beregningsdato og tidspunkt
        /// </summary>
        public DateTime BeregnetDato { get; set; } = DateTime.Now;
    }
}
