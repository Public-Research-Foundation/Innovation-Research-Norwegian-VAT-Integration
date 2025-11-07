using Innovation_Research_Norwegian_VAT_Integration.Modeller.Konstanter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Konfigurasjoner
{
    /// <summary>
    /// Konfigurasjon for skatteberegningsparametere
    /// Sentral plass for alle beregningsspesifikke verdier og strategier
    /// </summary>
    public class SkatteBeregningsKonfigurasjon
    {
        /// <summary>
        /// Inntektsgrenser for ulike skatteberegninger
        /// Definerer grenser for inntektsbaserte beregninger og strategier
        /// </summary>
        public InntektsGrenser InntektsGrenser { get; set; } = new InntektsGrenser();

        /// <summary>
        /// Skatte- og avgiftssatser for ulike beregninger
        /// Inneholder prosentsatser for skatter og avgifter
        /// </summary>
        public SkatteSatser Satser { get; set; } = new SkatteSatser();

        /// <summary>
        /// Fradragsgrenser for ulike utgiftstyper
        /// Definerer maksimumsgrenser for fradrag på ulike utgifter
        /// </summary>
        public FradragsGrenser FradragsGrenser { get; set; } = new FradragsGrenser();

        /// <summary>
        /// Strategiparametere for optimal lønn/utbytte beregning
        /// Inneholder parametere for skatteoptimaliseringsstrategier
        /// </summary>
        public OptimalLønnStrategi OptimalLønnStrategi { get; set; } = new OptimalLønnStrategi();

        /// <summary>
        /// Valideringsparametere for inndata
        /// Definerer grenser og regler for validering av input data
        /// </summary>
        public ValideringsParametere Validering { get; set; } = new ValideringsParametere();
    }

    /// <summary>
    /// Inntektsgrenser for skatteberegninger
    /// Definerer terskler for ulike inntektsbaserte beregninger
    /// </summary>
    public class InntektsGrenser
    {
        /// <summary>
        /// Grense for lav inntekt i ENK optimal lønn beregning
        /// 200 000 NOK - under denne anbefales høy andel som lønn
        /// </summary>
        public decimal LavInntektGrense { get; set; } = 200000m;

        /// <summary>
        /// Grense for middels inntekt i ENK optimal lønn beregning
        /// 500 000 NOK - mellom lav og middels brukes balansert strategi
        /// </summary>
        public decimal MiddelsInntektGrense { get; set; } = 500000m;

        /// <summary>
        /// Grense for høy inntekt i ENK optimal lønn beregning
        /// 1 000 000 NOK - over denne anbefales AS for bedre skatteoptimalisering
        /// </summary>
        public decimal HøyInntektGrense { get; set; } = 1000000m;

        /// <summary>
        /// Grense for å kvalifisere som pensjonist for skatteberegning
        /// 200 000 NOK - pensjonister under denne har andre regler
        /// </summary>
        public decimal PensjonistInntektGrense { get; set; } = 200000m;

        /// <summary>
        /// Grense for særskilt fradrag for lavinntekt
        /// 70 000 NOK - inntekt under denne gir ekstra fradrag
        /// </summary>
        public decimal LavinntektFradragGrense { get; set; } = 70000m;
    }

    /// <summary>
    /// Skatte- og avgiftssatser for beregninger
    /// Inneholder alle prosentsatser som brukes i skatteberegninger
    /// </summary>
    public class SkatteSatser
    {
        /// <summary>
        /// Prosentandel for lav inntekt i optimal lønn strategi
        /// 60% - ved lav inntekt anbefales 60% som lønn
        /// </summary>
        public decimal LavInntektLønnProsent { get; set; } = 0.6m;

        /// <summary>
        /// Prosentandel for middels inntekt i optimal lønn strategi
        /// 40% - ved middels inntekt anbefales 40% som lønn
        /// </summary>
        public decimal MiddelsInntektLønnProsent { get; set; } = 0.4m;

        /// <summary>
        /// Standard sats for beregning når ingen spesifikk sats er angitt
        /// 22% - brukes som fallback for kommunal skatt
        /// </summary>
        public decimal StandardSkatteprosent { get; set; } = 22.0m;

        /// <summary>
        /// Sats for fylkeskommunal skatt (i prosent)
        /// 0% - de fleste fylker har 0% fylkeskommunal skatt
        /// </summary>
        public decimal FylkeskommunalSkatt { get; set; } = 0.0m;

        /// <summary>
        /// Sats for fellesskatt (i prosent)
        /// 0% - fellesskatt er avviklet, men kan komme tilbake
        /// </summary>
        public decimal Fellesskatt { get; set; } = 0.0m;

        /// <summary>
        /// Sats for toppskatt (i prosent)
        /// 0% - toppskatt er avviklet, men kan komme tilbake
        /// </summary>
        public decimal Toppskatt { get; set; } = 0.0m;

        /// <summary>
        /// Sats for formuesskatt (i prosent)
        /// 1.0% - for formue over 1 700 000 NOK (2024)
        /// </summary>
        public decimal Formuesskatt { get; set; } = 1.0m;

        /// <summary>
        /// Grunnlag for formuesskatt (i NOK)
        /// 1 700 000 NOK - formue under dette er unntatt formuesskatt
        /// </summary>
        public decimal FormuesskattGrunnlag { get; set; } = 1700000m;
    }

    /// <summary>
    /// Fradragsgrenser for ulike utgiftstyper
    /// </summary>
    public class FradragsGrenser
    {
        /// <summary>
        /// Maksimalt fradrag for hjemmekontor per år
        /// </summary>
        public decimal MaksHjemmekontorFradrag { get; set; } = SkatteKonstanter.Fradragsgrenser.HjemmekontorMaks;

        /// <summary>
        /// Fradrag per kilometer for bilkjøring
        /// </summary>
        public decimal BilFradragPerKm { get; set; } = SkatteKonstanter.Fradragsgrenser.BilFradragPerKm;

        /// <summary>
        /// Maksimalt fradrag for reiseutgifter uten spesialgodkjenning
        /// 50 000 NOK - reiseutgifter over dette kan kreve ekstra dokumentasjon
        /// </summary>
        public decimal MaksReiseutgifter { get; set; } = 50000m;

        /// <summary>
        /// Maksimalt fradrag for kurs og opplæring per år
        /// 30 000 NOK - utgifter for kurs og faglig opplæring
        /// </summary>
        public decimal MaksKursFradrag { get; set; } = 30000m;

        /// <summary>
        /// Maksimalt fradrag for kontorrekvisita per år
        /// 10 000 NOK - for papir, skrivertoner, kontorrekvisita
        /// </summary>
        public decimal MaksKontorrekvisita { get; set; } = 10000m;

        /// <summary>
        /// Maksimalt fradrag for abonnement og medlemskap per år
        /// 15 000 NOK - for fagblad, bransjemedlemskap, programvare
        /// </summary>
        public decimal MaksAbonnementFradrag { get; set; } = 15000m;
    }

    /// <summary>
    /// Parametere for beregning av optimal lønn/utbytte fordeling
    /// Inneholder strategier for skatteoptimalisering i ENK-virksomheter
    /// </summary>
    public class OptimalLønnStrategi
    {
        /// <summary>
        /// Anbefalt lønn for middels inntekt
        /// 200 000 NOK - balansert lønn for skatteoptimalisering
        /// </summary>
        public decimal MiddelsInntektLønn { get; set; } = 200000m;

        /// <summary>
        /// Anbefalt lønn for høy inntekt
        /// 300 000 NOK - høyere lønn for å utnytte personfradrag
        /// </summary>
        public decimal HøyInntektLønn { get; set; } = 300000m;

        /// <summary>
        /// Minimumslønn for å oppnå trygdeytelser
        /// 69 900 NOK - minstelønn for å kvalifisere for trygdeytelser
        /// </summary>
        public decimal MinimumLønnForTrygdeytelser { get; set; } = 69900m;

        /// <summary>
        /// Optimal lønn for maksimal pensjonssparing
        /// 250 000 NOK - lønn som gir maksimal pensjonssparing
        /// </summary>
        public decimal OptimalLønnForPensjon { get; set; } = 250000m;

        /// <summary>
        /// Grense for å vurdere AS-omdanning
        /// 750 000 NOK - over denne bør AS vurderes for skatteoptimalisering
        /// </summary>
        public decimal ASOmdanningGrense { get; set; } = 750000m;

        /// <summary>
        /// Prosentandel for minstelønn i optimal strategi
        /// 25% - minimum andel som bør tas som lønn
        /// </summary>
        public decimal MinimumLønnProsent { get; set; } = 0.25m;

        /// <summary>
        /// Prosentandel for maksimum lønn i optimal strategi
        /// 75% - maksimum andel som bør tas som lønn
        /// </summary>
        public decimal MaksimumLønnProsent { get; set; } = 0.75m;
    }

    /// <summary>
    /// Valideringsparametere for inndata
    /// Sikrer at input data er innenfor rimelige grenser
    /// </summary>
    public class ValideringsParametere
    {
        /// <summary>
        /// Maksimalt tillatt inntektsbeløp for validering
        /// 10 000 000 NOK - inntekt over dette flagges som mulig feil
        /// </summary>
        public decimal MaksimalInntekt { get; set; } = 10000000m;

        /// <summary>
        /// Minimalt tillatt inntektsbeløp for validering
        /// 0 NOK - negativ inntekt er ikke tillatt
        /// </summary>
        public decimal MinimalInntekt { get; set; } = 0m;

        /// <summary>
        /// Maksimalt tillatt utgiftsbeløp for validering
        /// 5 000 000 NOK - utgifter over dette flagges som mulig feil
        /// </summary>
        public decimal MaksimalUtgift { get; set; } = 5000000m;

        /// <summary>
        /// Maksimalt tillatt antall kilometer for bilfradrag
        /// 100 000 km - over dette kreves spesialdokumentasjon
        /// </summary>
        public int MaksimaltAntallKilometer { get; set; } = 100000;

        /// <summary>
        /// Maksimalt tillatt antall ansatte for ENK
        /// 20 ansatte - over dette bør AS vurderes
        /// </summary>
        public int MaksimaltAntallAnsatte { get; set; } = 20;

        /// <summary>
        /// Maksimal alder for skatteberegning
        /// 150 år - alder over dette flagges som mulig feil
        /// </summary>
        public int MaksimalAlder { get; set; } = 150;

        /// <summary>
        /// Minimal alder for skatteberegning
        /// 0 år - nyfødte kan ha inntekt (f.eks. kapital)
        /// </summary>
        public int MinimalAlder { get; set; } = 0;
    }
}
