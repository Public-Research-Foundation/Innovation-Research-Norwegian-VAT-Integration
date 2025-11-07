using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons
{
    /// <summary>
    /// Resultat fra validering av virksomhetsutgifter
    /// Inneholder detaljert informasjon om gyldighet og anbefalinger
    /// </summary>
    public class VirksomhetsUtgiftValideringsResultat
    {
        /// <summary>
        /// Angir om alle utgiftene er gyldige ifølge norske skatteregler
        /// True: Alle utgifter er innenfor akseptable grenser
        /// False: Noen utgifter overstiger grenser eller er uvanlige
        /// </summary>
        public bool ErGyldig { get; set; }

        /// <summary>
        /// Liste over advarsler for utgifter som er høye, men ikke ulovlige
        /// Disse kan trenge ekstra dokumentasjon for Skattetaten
        /// </summary>
        public List<string> Advarsler { get; set; } = new List<string>();

        /// <summary>
        /// Liste over feil for utgifter som sannsynligvis ikke godkjennes
        /// Disse bør revideres eller fjernes
        /// </summary>
        public List<string> Feil { get; set; } = new List<string>();

        /// <summary>
        /// Liste over anbefalinger for forbedring av utgiftsføring
        /// Inkluderer tips for bedre dokumentasjon og fradrag
        /// </summary>
        public List<string> Anbefalinger { get; set; } = new List<string>();

        /// <summary>
        /// Detaljert oppdeling av utgifter i kategorier med valideringsresultat
        /// Gir oversikt over hver enkelt utgiftstype
        /// </summary>
        public List<VirksomhetsUtgiftKategori> UtgiftsKategorier { get; set; } = new List<VirksomhetsUtgiftKategori>();

        /// <summary>
        /// Total sum av alle utgifter som er validert
        /// </summary>
        public decimal TotaleUtgifter { get; set; }

        /// <summary>
        /// Antall utgiftskategorier som er validert
        /// </summary>
        public int AntallKategorier { get; set; }

        /// <summary>
        /// Antall kategorier som har advarsler eller feil
        /// </summary>
        public int AntallProblemer { get; set; }
    }
}
