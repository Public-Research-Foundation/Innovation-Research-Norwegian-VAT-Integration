using Innovation_Research_Norwegian_VAT_Integration.Modeller.Konstanter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Konfigurasjoner
{
    /// <summary>
    /// Hovedkonfigurasjon for hele skattebiblioteket
    /// Sentral plass for alle konfigurerbare parametere og alternativer
    /// </summary>
    public class NorskSkattAlternativer
    {
        /// <summary>
        /// Skatteåret beregningene gjelder for
        /// Format: "2024", "2025", etc.
        /// Påvirker hvilke satser og grenser som brukes
        /// </summary>
        public string Skatteår { get; set; } = "2024";

        /// <summary>
        /// Om kommunal skatt skal inkluderes i beregninger
        /// Kommunal skatt varierer mellom kommuner og er en vesentlig del av totalskatten
        /// </summary>
        public bool InkluderKommunalSkatt { get; set; } = true;

        /// <summary>
        /// Om kirkeskatt skal inkluderes i beregninger
        /// Kirkeskatt er frivillig og beregnes kun for medlemmer av Den norske kirke
        /// </summary>
        public bool InkluderKirkeskatt { get; set; } = false;

        /// <summary>
        /// Standard skatteprosent som brukes når ingen spesifikk sats er angitt
        /// Brukes som fallback ved manglende kommunedata
        /// </summary>
        public decimal StandardSkatteprosent { get; set; } = 22.0m;

        /// <summary>
        /// Kommunale skattesatser per kommunenummer
        /// Nøkkel: 4-sifret kommunenummer (f.eks. "0301")
        /// Verdi: Skatteprosent for den kommunen
        /// </summary>
        public Dictionary<string, decimal> KommunaleSkattesatser { get; set; } = new Dictionary<string, decimal>();

        /// <summary>
        /// Konfigurasjon for trygdeavgiftsberegninger
        /// Inneholder satser og grenser for trygdeavgift
        /// </summary>
        public TrygdeAvgiftAlternativer Trygdeavgift { get; set; } = new TrygdeAvgiftAlternativer();

        /// <summary>
        /// Konfigurasjon for MVA-beregninger
        /// Inneholder alle norske MVA-satser
        /// </summary>
        public MvaAlternativer MvaInnstillinger { get; set; } = new MvaAlternativer();

        /// <summary>
        /// Konfigurasjon for ENK-spesifikke beregninger
        /// Inneholder regler og parametere for Enkeltpersonforetak
        /// </summary>
        public EnkAlternativer EnkInnstillinger { get; set; } = new EnkAlternativer();

        /// <summary>
        /// Generell konfigurasjon for skatteberegningsparametere
        /// Inneholder grenser, satser og strategiparametere for alle beregninger
        /// </summary>
        public SkatteBeregningsKonfigurasjon BeregningsKonfigurasjon { get; set; } = new SkatteBeregningsKonfigurasjon();

        /// <summary>
        /// Om detaljert logging skal aktiveres
        /// Gir mer utførlige loggmeldinger for feilsøking og overvåkning
        /// </summary>
        public bool AktiverDetaljertLogging { get; set; } = false;

        /// <summary>
        /// Om hendelser skal utløses for beregninger
        /// Deaktiver for bedre ytelse hvis hendelser ikke brukes
        /// </summary>
        public bool AktiverHendelser { get; set; } = true;

        /// <summary>
        /// Maksimal tillatt inntekt for beregninger
        /// Sikkerhetsgrense for å forhindre feil i store beregninger
        /// </summary>
        public decimal MaksimalTillattInntekt { get; set; } = 10000000m; // 10 millioner NOK
    }
}
