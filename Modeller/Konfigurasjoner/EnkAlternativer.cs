using Innovation_Research_Norwegian_VAT_Integration.Modeller.Konstanter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Konfigurasjoner
{
    /// <summary>
    /// ENK-spesifikk konfigurasjon for selvstendig næringsdrivende
    /// </summary>
    public class EnkAlternativer
    {
        /// <summary>
        /// Sats for standardfradrag (minifradrag) - 43% for inntekt under 200 000 NOK
        /// </summary>
        public decimal StandardFradragsSats { get; set; } = SkatteKonstanter.Minifradrag.Sats;

        /// <summary>
        /// Maksimalt standardfradrag - 86 000 NOK for inntekt over 200 000 NOK
        /// </summary>
        public decimal MaksStandardFradrag { get; set; } = SkatteKonstanter.Minifradrag.MaksFradrag;

        /// <summary>
        /// Inntektsgrense for standardfradrag
        /// 200 000 NOK - over denne grensen gis kun maksfradrag
        /// </summary>
        public decimal InntektsgrenseStandardFradrag { get; set; } = 200000m;


        /// <summary>
        /// Trygdeavgift sats for selvstendig næringsdrivende - 11.4%
        /// </summary>
        public decimal TrygdeavgiftSats { get; set; } = SkatteKonstanter.Trygdeavgift.SatsSelvstendig;

        /// <summary>
        /// Minste inntekt for trygdeavgift - 69 900 NOK
        /// </summary>
        public decimal MinsteTrygdeavgiftInntekt { get; set; } = SkatteKonstanter.Trygdeavgift.NedreGrense;

        /// <summary>
        /// Maksimal inntekt for trygdeavgift - 750 000 NOK
        /// </summary>
        public decimal MaksTrygdeavgiftInntekt { get; set; } = SkatteKonstanter.Trygdeavgift.ØvreGrense;

        /// <summary>
        /// Maksimalt fradrag for hjemmekontor per år
        /// 5 000 NOK for dokumenterte hjemmekontorutgifter
        /// </summary>
        public decimal MaksHjemmekontorFradrag { get; set; } = 5000m;

        /// <summary>
        /// Fradrag per kilometer for bilkjøring i virksomhetsøyemed
        /// 3.70 NOK per kilometer (2024)
        /// </summary>
        public decimal MaksBilFradragPerKm { get; set; } = 3.70m;

        /// <summary>
        /// Maksimalt antall kilometer for bilfradrag uten spesialgodkjenning
        /// 50 000 km - over dette kan kreve ekstra dokumentasjon
        /// </summary>
        public int MaksKilometerUtenSpesialgodkjenning { get; set; } = 50000;

        /// <summary>
        /// Bransjespesifikke satser for ulike næringer
        /// Nøkkel: Bransjekode (f.eks. "IT", "BYGG", "RESTURANT")
        /// Verdi: Spesifikk sats eller fradrag for den bransjen
        /// </summary>
        public Dictionary<string, decimal> BransjeSpesifikkeSatser { get; set; } = new Dictionary<string, decimal>();

        /// <summary>
        /// Om tapsoverføring fra tidligere år er tillatt
        /// Ifølge norsk skattelov kan tap overføres i opptil 5 år
        /// </summary>
        public bool TillatTapOverførsel { get; set; } = true;

        /// <summary>
        /// Maksimalt antall år for tapsoverføring
        /// 5 år - tap kan bare overføres 5 år tilbake i tid
        /// </summary>
        public int MaksTapOverførselÅr { get; set; } = 5;

        /// <summary>
        /// Prosentandel av tap som kan overføres per år
        /// 100% - hele tapet kan overføres, men noen begrensninger kan gjelde
        /// </summary>
        public decimal TapOverførselProsent { get; set; } = 1.0m;

        /// <summary>
        /// Maksimalt fradrag for representasjon per år
        /// 20 000 NOK for dokumenterte representasjonsutgifter
        /// </summary>
        public decimal MaksRepresentasjonsFradrag { get; set; } = 20000m;

        /// <summary>
        /// Maksimalt fradrag for gaver til ansatte per år
        /// 5 000 NOK per ansatt for gaver og personalgoder
        /// </summary>
        public decimal MaksGaveFradrag { get; set; } = 5000m;

        /// <summary>
        /// Sats for avskrivning på kontorutstyr (i prosent)
        /// 30% - kontorutstyr kan avskrives med 30% per år
        /// </summary>
        public decimal AvskrivningKontorutstyr { get; set; } = 0.30m;

        /// <summary>
        /// Sats for avskrivning på kjøretøy (i prosent)
        /// 20% - kjøretøy kan avskrives med 20% per år
        /// </summary>
        public decimal AvskrivningKjøretøy { get; set; } = 0.20m;
    }
}
