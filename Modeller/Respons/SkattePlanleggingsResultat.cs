using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons
{
    /// <summary>
    /// Resultat fra skatteplanlegging for ENK-virksomheter
    /// Inneholder strategier og anbefalinger for skatteoptimalisering
    /// </summary>
    public class SkattePlanleggingsResultat
    {
        /// <summary>
        /// Anbefalte strategier for skatteplanlegging
        /// Konkrete tiltak organisert etter prioritet og effekt
        /// </summary>
        public string[] Strategier { get; set; }

        /// <summary>
        /// Anbefalt tidsramme for implementering av strategiene
        /// F.eks. "Kvartalsvis", "Månedlig", "Umiddelbart"
        /// </summary>
        public string AnbefaltTidsramme { get; set; }

        /// <summary>
        /// Estimert skattebesparelse med implementerte strategier
        /// Beregnet årlig besparelse i NOK
        /// </summary>
        public decimal EstimertBesparelse { get; set; }

        /// <summary>
        /// Risikovurdering av de foreslåtte strategiene
        /// Lav: Minimal risiko for tilsyn
        /// Middels: Noe risiko, men akseptabel
        /// Høy: Betydelig risiko, kan utløse tilsyn
        /// </summary>
        public string RisikoVurdering { get; set; }

        /// <summary>
        /// Investeringsbehov for å implementere strategiene
        /// F.eks. kostnad for nytt utstyr, systemer eller konsulent
        /// </summary>
        public decimal Investeringsbehov { get; set; }

        /// <summary>
        /// Estimert tilbakebetalingstid for investeringer
        /// Antall år før investeringen lønner seg gjennom skattebesparelser
        /// </summary>
        public decimal Tilbakebetalingstid { get; set; }

        /// <summary>
        /// Prioriteringsnivå for hver strategi
        /// Høy: Stor effekt, lav kostnad - bør implementeres umiddelbart
        /// Middels: God effekt, moderat kostnad - bør implementeres innen 6 måneder
        /// Lav: Begrenset effekt, høy kostnad - vurder ved revisjon
        /// </summary>
        public Dictionary<string, string> StrategiPrioritering { get; set; } = new Dictionary<string, string>();
    }
}
