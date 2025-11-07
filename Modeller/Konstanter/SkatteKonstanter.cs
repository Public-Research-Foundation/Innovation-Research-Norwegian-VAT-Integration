using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Konstanter
{
    /// <summary>
    /// Statiske konstanter for norske skatteverdier som sjelden endres
    /// Disse verdiene brukes som fallback hvis ikke konfigurasjon er angitt
    /// </summary>
    public static class SkatteKonstanter
    {
        /// <summary>
        /// Standard skatteår for beregninger
        /// </summary>
        public const int StandardSkatteår = 2024;

        /// <summary>
        /// MVA satser for Norge i henhold til merverdiavgiftsloven
        /// Disse satsene er fastsatt av Finansdepartementet
        /// </summary>
        public static class MvaSatser
        {
            /// <summary>
            /// Standard MVA sats for de fleste varer og tjenester
            /// 25% gjelder for de fleste kommersielle transaksjoner
            /// </summary>
            public const decimal Standard = 25.0m;

            /// <summary>
            /// Redusert MVA sats for spesielle varer og tjenester
            /// 15% gjelder for bl.a. næringsmidler, persontransport
            /// </summary>
            public const decimal Redusert = 15.0m;

            /// <summary>
            /// MVA sats for matvarer ifølge matvareforskriften
            /// 15% for alle matvarer med noen spesielle unntak
            /// </summary>
            public const decimal Matvarer = 15.0m;

            /// <summary>
            /// MVA sats for fisk og fiskeprodukter
            /// 15% for fisk, skalldyr og andre fiskeprodukter
            /// </summary>
            public const decimal Fisk = 15.0m;

            /// <summary>
            /// MVA sats for persontransporttjenester
            /// 15% for buss, tog, taxi, flybilletter innenlands
            /// </summary>
            public const decimal Persontransport = 15.0m;

            /// <summary>
            /// MVA sats for overnattingstjenester
            /// 15% for hotell, camping, og annen overnatting
            /// </summary>
            public const decimal Overnatting = 15.0m;

            /// <summary>
            /// Lav MVA sats for spesielle kulturelle tjenester
            /// 12% for kinobilletter, fotballkamper, kulturarrangementer
            /// </summary>
            public const decimal Lav = 12.0m;
        }

        /// <summary>
        /// Trygdeavgift satser og grenser ifølge folketrygdloven
        /// Disse verdiene justeres årlig av Arbeids- og velferdsdirektoratet
        /// </summary>
        public static class Trygdeavgift
        {
            /// <summary>
            /// Sats for trygdeavgift for arbeidstakere
            /// 8.2% for inntekt mellom nedre og øvre grense
            /// </summary>
            public const decimal SatsArbeidstaker = 8.2m;

            /// <summary>
            /// Sats for trygdeavgift for selvstendig næringsdrivende
            /// 11.4% for inntekt mellom nedre og øvre grense
            /// Høyere enn arbeidstakere pga manglende arbeidsgiveravgift
            /// </summary>
            public const decimal SatsSelvstendig = 11.4m;

            /// <summary>
            /// Sats for trygdeavgift for pensjonister
            /// 5.1% for pensjonsinntekt mellom nedre og øvre grense
            /// </summary>
            public const decimal SatsPensjonist = 5.1m;

            /// <summary>
            /// Nedre grense for trygdeavgift (G)
            /// 69 900 NOK for 2024 - inntekt under dette er unntatt
            /// </summary>
            public const decimal NedreGrense = 69900m;

            /// <summary>
            /// Øvre grense for trygdeavgift (6G)
            /// 750 000 NOK for 2024 - inntekt over dette er unntatt
            /// </summary>
            public const decimal ØvreGrense = 750000m;

            /// <summary>
            /// Grunnbeløp i folketrygden (G)
            /// 125 000 NOK for 2024 - brukes for mange ytelser
            /// </summary>
            public const decimal Grunnbeløp = 125000m;
        }

        /// <summary>
        /// Minifradrag (standardfradrag) for ENK ifølge skatteloven
        /// Forenklet fradragsordning for små virksomheter
        /// </summary>
        public static class Minifradrag
        {
            /// <summary>
            /// Sats for minifradrag for inntekt under grensen
            /// 43% av inntekt opp til 200 000 NOK
            /// </summary>
            public const decimal Sats = 0.43m;

            /// <summary>
            /// Inntektsgrense for fullt minifradrag
            /// 200 000 NOK - over denne gis kun maksfradrag
            /// </summary>
            public const decimal Inntektsgrense = 200000m;

            /// <summary>
            /// Maksimalt minifradrag uavhengig av inntekt
            /// 86 000 NOK (43% av 200 000 NOK)
            /// </summary>
            public const decimal MaksFradrag = 86000m;
        }

        /// <summary>
        /// Diverse fradragsgrenser ifølge norsk skattelovgivning
        /// Disse grensene er fastsatt av Skattedirektoratet
        /// </summary>
        public static class Fradragsgrenser
        {
            /// <summary>
            /// Maksimalt fradrag for hjemmekontor per år
            /// 5 000 NOK for dokumenterte hjemmekontorutgifter
            /// </summary>
            public const decimal HjemmekontorMaks = 5000m;

            /// <summary>
            /// Fradrag per kilometer for bilkjøring i virksomhetsøyemed
            /// 3.70 NOK per kilometer (2024)
            /// </summary>
            public const decimal BilFradragPerKm = 3.70m;

            /// <summary>
            /// Maksimalt fradrag for representasjon per år
            /// 20 000 NOK for dokumenterte representasjonsutgifter
            /// </summary>
            public const decimal RepresentasjonMaks = 20000m;

            /// <summary>
            /// Maksimalt fradrag for gaver til ansatte per år
            /// 5 000 NOK per ansatt for gaver og personalgoder
            /// </summary>
            public const decimal GaveMaks = 5000m;

            /// <summary>
            /// Maksimalt fradrag for kurs og opplæring per år
            /// 30 000 NOK for dokumenterte kursutgifter
            /// </summary>
            public const decimal KursMaks = 30000m;

            /// <summary>
            /// Maksimalt fradrag for reiseutgifter uten spesialgodkjenning
            /// 50 000 NOK for dokumenterte reiseutgifter
            /// </summary>
            public const decimal ReiseMaks = 50000m;
        }

        /// <summary>
        /// Satser for avskrivning på ulike typer eiendeler
        /// Ifølge regnskapsloven og skatteloven
        /// </summary>
        public static class Avskrivningssatser
        {
            /// <summary>
            /// Sats for avskrivning på kontorutstyr
            /// 30% per år - datamaskiner, møbler, telefoner
            /// </summary>
            public const decimal Kontorutstyr = 0.30m;

            /// <summary>
            /// Sats for avskrivning på kjøretøy
            /// 20% per år - biler, lastebiler, motorsykler
            /// </summary>
            public const decimal Kjøretøy = 0.20m;

            /// <summary>
            /// Sats for avskrivning på maskiner og verktøy
            /// 25% per år - produksjonsutstyr, verktøy
            /// </summary>
            public const decimal Maskiner = 0.25m;

            /// <summary>
            /// Sats for avskrivning på bygninger
            /// 4% per år - kontorbygg, lager, butikklokaler
            /// </summary>
            public const decimal Bygninger = 0.04m;

            /// <summary>
            /// Sats for avskrivning på inventar og innredning
            /// 15% per år - butikkinventar, spesialinnredning
            /// </summary>
            public const decimal Inventar = 0.15m;
        }

        /// <summary>
        /// Generelle skattegrenser og -satser
        /// Brukes i ulike skatteberegninger
        /// </summary>
        public static class GenerelleSatser
        {
            /// <summary>
            /// Standard kommunal skattesats
            /// 22% - brukes som referanse for kommunal skatt
            /// </summary>
            public const decimal StandardKommunalSkatt = 22.0m;

            /// <summary>
            /// Grense for særskilt fradrag for lavinntekt
            /// 70 000 NOK - inntekt under denne gir ekstra fradrag
            /// </summary>
            public const decimal LavinntektFradragGrense = 70000m;

            /// <summary>
            /// Grense for personfradrag (minstefradrag)
            /// 69 900 NOK - minimumsfradrag for alle skattytere
            /// </summary>
            public const decimal Personfradrag = 69900m;

            /// <summary>
            /// Grense for skattemessig minstelønn
            /// 69 900 NOK - minimum for trygdeavgift og andre beregninger
            /// </summary>
            public const decimal Minstelønn = 69900m;

            /// <summary>
            /// Maksimal alder for barn som gir skattefradrag
            /// 18 år - barn over denne alderen gir ikke fradrag
            /// </summary>
            public const int MaksAlderBarnFradrag = 18;
        }

    }
}