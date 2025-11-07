using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Hendelser
{

    /// <summary>
    /// Argumenter for feilhendelser i skatteberegning
    /// Utløses ved exceptions og andre feil i beregningsprosessen
    /// </summary>
    public class SkatteFeilHendelseArgs : EventArgs
    {
        /// <summary>
        /// Unntaket som oppstod under beregningen
        /// Inneholder detaljert feilinformasjon og stack trace
        /// </summary>
        public Exception Unntak { get; set; }

        /// <summary>
        /// Operasjonen som feilet
        /// F.eks. "BeregnSkattAsync", "BeregnMvaAsync"
        /// </summary>
        public string Operasjon { get; set; }

        /// <summary>
        /// Kontekstinformasjon for feilen
        /// Inneholder relevant data som kan hjelpe med feilsøking
        /// </summary>
        public Dictionary<string, object> Kontekst { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Tidspunktet for feilen i UTC
        /// </summary>
        public DateTime Tidsstempel { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Unik identifikator for feilhendelsen
        /// </summary>
        public string FeilId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Alvorlighetsgrad for feilen
        /// Info: Mindre feil som ikke påvirker funksjonaliteten
        /// Warning: Feil som kan påvirke resultatet
        /// Error: Alvorlige feil som hindrer beregning
        /// </summary>
        public string Alvorlighetsgrad { get; set; } = "Error";
    }

}
