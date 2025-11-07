using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Hendelser
{
    /// <summary>
    /// Argumenter for ENK-utgiftsvalideringshendelser
    /// Utløses ved validering av virksomhetsutgifter for ENK
    /// </summary>
    public class EnkUtgiftValideringHendelseArgs : EventArgs
    {
        /// <summary>
        /// Forespørselen som ble brukt for validering
        /// Inneholder utgiftsdata som skal valideres
        /// </summary>
        public EnkBeregningForesporsel Forespørsel { get; set; }

        /// <summary>
        /// Resultatet av valideringen
        /// Inneholder valideringsresultater og anbefalinger
        /// </summary>
        public VirksomhetsUtgiftValideringsResultat ValideringsResultat { get; set; }

        /// <summary>
        /// Tidspunktet for valideringen i UTC
        /// </summary>
        public DateTime Tidsstempel { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Unik identifikator for valideringssesjonen
        /// </summary>
        public string SesjonId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Antall utgiftskategorier som ble validert
        /// </summary>
        public int AntallKategorier { get; set; }

        /// <summary>
        /// Antall advarsler og feil som ble identifisert
        /// </summary>
        public int AntallProblemer { get; set; }
    }

}
