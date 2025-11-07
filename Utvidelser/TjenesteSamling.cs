using Innovation_Research_Norwegian_VAT_Integration.Grensesnitt;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Konfigurasjoner;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Konstanter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Utvidelser
{
    /// <summary>
    /// Utvidelsesmetoder for IServiceCollection for enkel registrering av tjenester
    /// Gir et fluent API for konfigurasjon av Norsk Skattebibliotek
    /// </summary>
    public static class TjenesteSamlingUtvidelser
    {
        /// <summary>
        /// Registrerer norsk skattetjeneste og dens avhengigheter i DI-containeren
        /// Konfigurerer alle nødvendige tjenester for skatteberegninger
        /// </summary>
        /// <param name="tjenester">Tjenestesamlingen som skal utvides</param>
        /// <param name="konfigurerAlternativer">Valgfri handling for å konfigurere alternativer</param>
        /// <returns>Tjenestesamlingen for method chaining</returns>
        /// <example>
        /// // Grunnleggende bruk:
        /// services.LeggTilNorskSkattTjeneste();
        /// 
        /// // Avansert bruk med konfigurasjon:
        /// services.LeggTilNorskSkattTjeneste(alternativer =>
        /// {
        ///     alternativer.Skatteår = "2024";
        ///     alternativer.InkluderKommunalSkatt = true;
        /// });
        /// </example>
        public static IServiceCollection LeggTilNorskSkattTjeneste(
            this IServiceCollection tjenester,
            Action<NorskSkattAlternativer> konfigurerAlternativer = null)
        {
            // Valider input
            if (tjenester == null)
                throw new ArgumentNullException(nameof(tjenester));

            // Konfigurer alternativer med IOptions pattern
            tjenester.Configure<NorskSkattAlternativer>(alternativer =>
            {
                // Kjern brukerens konfigurasjon først
                konfigurerAlternativer?.Invoke(alternativer);

                // Sett standardverdier for ikke-konfigurerte alternativer
                SettStandardAlternativer(alternativer);
            });

            // Registrer hovedtjenesten som singleton
            tjenester.AddSingleton<INorskSkatteTjeneste, NorskSkatteTjeneste>();

            // Registrer logging hvis ikke allerede registrert
            if (!tjenester.Any(x => x.ServiceType == typeof(ILogger<>)))
            {
                tjenester.AddLogging();
            }

            return tjenester;
        }

        /// <summary>
        /// Registrerer ENK-skattetjenester og dens avhengigheter i DI-containeren
        /// Konfigurerer spesifikke tjenester for Enkeltpersonforetak
        /// </summary>
        /// <param name="tjenester">Tjenestesamlingen som skal utvides</param>
        /// <param name="konfigurerEnkAlternativer">Valgfri handling for å konfigurere ENK-alternativer</param>
        /// <returns>Tjenestesamlingen for method chaining</returns>
        /// <example>
        /// // Grunnleggende bruk:
        /// services.LeggTilNorskEnkSkattTjenester();
        /// 
        /// // Avansert bruk med konfigurasjon:
        /// services.LeggTilNorskEnkSkattTjenester(alternativer =>
        /// {
        ///     alternativer.StandardFradragsSats = 0.43m;
        ///     alternativer.MaksStandardFradrag = 86000m;
        /// });
        /// </example>
        public static IServiceCollection LeggTilNorskEnkSkattTjenester(
            this IServiceCollection tjenester,
            Action<EnkAlternativer> konfigurerEnkAlternativer = null)
        {
            // Valider input
            if (tjenester == null)
                throw new ArgumentNullException(nameof(tjenester));

            // Konfigurer ENK-spesifikke alternativer
            tjenester.Configure<NorskSkattAlternativer>(alternativer =>
            {
                // Kjern brukerens konfigurasjon for ENK-alternativer
                konfigurerEnkAlternativer?.Invoke(alternativer.EnkInnstillinger);

                // Sett standardverdier for ENK-spesifikke alternativer
                SettStandardEnkAlternativer(alternativer.EnkInnstillinger);
            });

            // Registrer ENK-tjenesten som singleton
            // Merk: Krever at INorskSkatteTjeneste er registrert først
            tjenester.AddSingleton<IEnkSkattTjeneste, EnkSkattTjeneste>();

            return tjenester;
        }

        /// <summary>
        /// Setter standardalternativer for skattebiblioteket
        /// Fyller ut manglende verdier med konstantene fra SkatteKonstanter
        /// </summary>
        /// <param name="alternativer">Alternativer som skal fylles ut</param>
        private static void SettStandardAlternativer(NorskSkattAlternativer alternativer)
        {
            // Sett standard skatteår hvis ikke spesifisert
            if (string.IsNullOrEmpty(alternativer.Skatteår))
            {
                alternativer.Skatteår = SkatteKonstanter.StandardSkatteår.ToString();
            }

            // Sett standard kommunale skattesatser hvis ikke angitt
            if (alternativer.KommunaleSkattesatser?.Count == 0)
            {
                alternativer.KommunaleSkattesatser = new Dictionary<string, decimal>
                {
                    ["0301"] = SkatteKonstanter.GenerelleSatser.StandardKommunalSkatt, // Oslo
                    ["4601"] = SkatteKonstanter.GenerelleSatser.StandardKommunalSkatt, // Bergen
                    ["5001"] = SkatteKonstanter.GenerelleSatser.StandardKommunalSkatt, // Trondheim
                    ["1101"] = SkatteKonstanter.GenerelleSatser.StandardKommunalSkatt, // Stavanger
                    ["5401"] = SkatteKonstanter.GenerelleSatser.StandardKommunalSkatt  // Tromsø
                };
            }

            // Sett standard trygdeavgiftsalternativer hvis ikke konfigurert
            if (alternativer.Trygdeavgift != null)
            {
                alternativer.Trygdeavgift.Sats = alternativer.Trygdeavgift.Sats > 0
                    ? alternativer.Trygdeavgift.Sats
                    : SkatteKonstanter.Trygdeavgift.SatsArbeidstaker;

                alternativer.Trygdeavgift.SatsSelvstendig = alternativer.Trygdeavgift.SatsSelvstendig > 0
                    ? alternativer.Trygdeavgift.SatsSelvstendig
                    : SkatteKonstanter.Trygdeavgift.SatsSelvstendig;

                alternativer.Trygdeavgift.NedreGrense = alternativer.Trygdeavgift.NedreGrense > 0
                    ? alternativer.Trygdeavgift.NedreGrense
                    : SkatteKonstanter.Trygdeavgift.NedreGrense;

                alternativer.Trygdeavgift.ØvreGrense = alternativer.Trygdeavgift.ØvreGrense > 0
                    ? alternativer.Trygdeavgift.ØvreGrense
                    : SkatteKonstanter.Trygdeavgift.ØvreGrense;
            }

            // Sett standard MVA-alternativer hvis ikke konfigurert
            if (alternativer.MvaInnstillinger != null)
            {
                alternativer.MvaInnstillinger.StandardSats = alternativer.MvaInnstillinger.StandardSats > 0
                    ? alternativer.MvaInnstillinger.StandardSats
                    : SkatteKonstanter.MvaSatser.Standard;

                alternativer.MvaInnstillinger.RedusertSats = alternativer.MvaInnstillinger.RedusertSats > 0
                    ? alternativer.MvaInnstillinger.RedusertSats
                    : SkatteKonstanter.MvaSatser.Redusert;

                alternativer.MvaInnstillinger.MatvareSats = alternativer.MvaInnstillinger.MatvareSats > 0
                    ? alternativer.MvaInnstillinger.MatvareSats
                    : SkatteKonstanter.MvaSatser.MatvareSats;
            }

            // Sett maksimal tillatt inntekt hvis ikke spesifisert
            if (alternativer.MaksimalTillattInntekt <= 0)
            {
                alternativer.MaksimalTillattInntekt = 10000000m; // 10 millioner NOK
            }
        }

        /// <summary>
        /// Setter standardalternativer for ENK-spesifikke beregninger
        /// Fyller ut manglende verdier med konstantene fra SkatteKonstanter
        /// </summary>
        /// <param name="alternativer">ENK-alternativer som skal fylles ut</param>
        private static void SettStandardEnkAlternativer(EnkAlternativer alternativer)
        {
            // Sett standard fradragssatser hvis ikke spesifisert
            alternativer.StandardFradragsSats = alternativer.StandardFradragsSats > 0
                ? alternativer.StandardFradragsSats
                : SkatteKonstanter.Minifradrag.Sats;

            alternativer.MaksStandardFradrag = alternativer.MaksStandardFradrag > 0
                ? alternativer.MaksStandardFradrag
                : SkatteKonstanter.Minifradrag.MaksFradrag;

            alternativer.InntektsgrenseStandardFradrag = alternativer.InntektsgrenseStandardFradrag > 0
                ? alternativer.InntektsgrenseStandardFradrag
                : SkatteKonstanter.Minifradrag.Inntektsgrense;

            // Sett standard trygdeavgift for selvstendig
            alternativer.TrygdeavgiftSats = alternativer.TrygdeavgiftSats > 0
                ? alternativer.TrygdeavgiftSats
                : SkatteKonstanter.Trygdeavgift.SatsSelvstendig;

            alternativer.MinsteTrygdeavgiftInntekt = alternativer.MinsteTrygdeavgiftInntekt > 0
                ? alternativer.MinsteTrygdeavgiftInntekt
                : SkatteKonstanter.Trygdeavgift.NedreGrense;

            alternativer.MaksTrygdeavgiftInntekt = alternativer.MaksTrygdeavgiftInntekt > 0
                ? alternativer.MaksTrygdeavgiftInntekt
                : SkatteKonstanter.Trygdeavgift.ØvreGrense;

            // Sett standard fradragsgrenser
            alternativer.MaksHjemmekontorFradrag = alternativer.MaksHjemmekontorFradrag > 0
                ? alternativer.MaksHjemmekontorFradrag
                : SkatteKonstanter.Fradragsgrenser.HjemmekontorMaks;

            alternativer.MaksBilFradragPerKm = alternativer.MaksBilFradragPerKm > 0
                ? alternativer.MaksBilFradragPerKm
                : SkatteKonstanter.Fradragsgrenser.BilFradragPerKm;

            // Sett standard avskrivningssatser
            alternativer.AvskrivningKontorutstyr = alternativer.AvskrivningKontorutstyr > 0
                ? alternativer.AvskrivningKontorutstyr
                : SkatteKonstanter.Avskrivningssatser.Kontorutstyr;

            alternativer.AvskrivningKjøretøy = alternativer.AvskrivningKjøretøy > 0
                ? alternativer.AvskrivningKjøretøy
                : SkatteKonstanter.Avskrivningssatser.Kjøretøy;

            // Sett standard grenser for tapsoverføring
            if (alternativer.MaksTapOverførselÅr <= 0)
            {
                alternativer.MaksTapOverførselÅr = 5;
            }

            if (alternativer.TapOverførselProsent <= 0)
            {
                alternativer.TapOverførselProsent = 1.0m;
            }
        }
    }
}
