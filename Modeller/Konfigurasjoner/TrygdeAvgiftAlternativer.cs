using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Konfigurasjoner
{
    /// <summary>
    /// Konfigurasjon for trygdeavgiftsberegninger
    /// Definerer satser og grenser for trygdeavgift i henhold til norske regler
    /// </summary>
    public class TrygdeAvgiftAlternativer
    {
        /// <summary>
        /// Sats for trygdeavgift for arbeidstakere (i prosent)
        /// 8.2% for inntekt mellom 69 900 NOK og 750 000 NOK (2024)
        /// </summary>
        public decimal Sats { get; set; } = 8.2m;

        /// <summary>
        /// Sats for trygdeavgift for selvstendig næringsdrivende (i prosent)
        /// 11.4% for inntekt mellom 69 900 NOK og 750 000 NOK (2024)
        /// </summary>
        public decimal SatsSelvstendig { get; set; } = 11.4m;

        /// <summary>
        /// Nedre grense for trygdeavgift (i NOK)
        /// Inntekt under denne grensen er unntatt trygdeavgift
        /// 69 900 NOK for 2024
        /// </summary>
        public decimal NedreGrense { get; set; } = 69900m;

        /// <summary>
        /// Øvre grense for trygdeavgift (i NOK)
        /// Inntekt over denne grensen er unntatt trygdeavgift
        /// 750 000 NOK for 2024
        /// </summary>
        public decimal ØvreGrense { get; set; } = 750000m;

        /// <summary>
        /// Om trygdeavgift skal beregnes trinnvis
        /// True: Beregnes kun på beløp mellom nedre og øvre grense
        /// False: Beregnes på hele inntekten (ikke anbefalt)
        /// </summary>
        public bool BeregnTrinnvis { get; set; } = true;

        /// <summary>
        /// Justeringsfaktor for trygdeavgift (for testing og simulering)
        /// 1.0 = normal sats, 0.5 = halv sats, etc.
        /// </summary>
        public decimal Justeringsfaktor { get; set; } = 1.0m;
    }
}
