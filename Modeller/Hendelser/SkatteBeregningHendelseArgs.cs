using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel.NorskSkatteBibliotek.Models.Requests;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Hendelser
{
    /// <summary>
    /// Argumenter for skatteberegningshendelser
    /// Brukes for hendelsene FørSkatteberegning og EtterSkatteberegning
    /// </summary>
    public class SkatteBeregningHendelseArgs : EventArgs
    {
        /// <summary>
        /// Forespørselen som ble brukt for beregningen
        /// Inneholder all input data for skatteberegningen
        /// </summary>
        public SkatteBeregningsForesporsel Forespørsel { get; set; }

        /// <summary>
        /// Resultatet av beregningen
        /// Inneholder alle beregnede verdier og analyser
        /// </summary>
        public SkatteBeregningsResultat Resultat { get; set; }

        /// <summary>
        /// Tidspunktet for hendelsen i UTC
        /// Brukes for logging og sekvensering av hendelser
        /// </summary>
        public DateTime Tidsstempel { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Unik identifikator for beregningssesjonen
        /// Brukes for å spore relaterte hendelser
        /// </summary>
        public string SesjonId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Type beregning som ble utført
        /// F.eks. "PersonligSkatt", "Trygdeavgift", "MVA"
        /// </summary>
        public string Beregningstype { get; set; }
    }
}
}
