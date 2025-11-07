using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Hendelser
{
    /// <summary>
    /// Argumenter for ENK-beregningshendelser
    /// Spesialisert hendelse for ENK-spesifikke beregninger
    /// </summary>
    public class EnkBeregningHendelseArgs : EventArgs
    {
        /// <summary>
        /// Forespørselen som ble brukt for ENK-beregningen
        /// Inneholder ENK-spesifikke data som inntekter og utgifter
        /// </summary>
        public EnkBeregningForespørsel Forespørsel { get; set; }

        /// <summary>
        /// Resultatet av ENK-beregningen
        /// Inneholder ENK-spesifikke beregninger og analyser
        /// </summary>
        public EnkBeregningResultat Resultat { get; set; }

        /// <summary>
        /// Tidspunktet for hendelsen i UTC
        /// </summary>
        public DateTime Tidsstempel { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Unik identifikator for beregningssesjonen
        /// </summary>
        public string SesjonId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Type ENK-beregning som ble utført
        /// F.eks. "KomplettBeregning", "OptimalLønn", "UtgiftsValidering"
        /// </summary>
        public string Beregningstype { get; set; }
    }
}
