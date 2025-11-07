using Microsoft.Extensions.DependencyInjection;
using Innovation_Research_Norwegian_VAT_Integration.Grensesnitt;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Konfigurasjoner;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Konstanter;
using System;
using System.Collections.Generic;
using System.Text;
using Innovation_Research_Norwegian_VAT_Integration.Tjenester;

namespace Innovation_Research_Norwegian_VAT_Integration.Utvidelser
{
    /// <summary>
    /// Utvidelsesmetoder for IServiceCollection for enkel registrering av tjenester
    /// </summary>
    public static class TjenesteSamlingUtvidelser
    {
        /// <summary>
        /// Registrerer norsk skattetjeneste og dens avhengigheter
        /// </summary>
        /// <param name="tjenester">Tjenestesamlingen</param>
        /// <param name="konfigurerAlternativer">Valgfri konfigurasjon av alternativer</param>
        /// <returns>Tjenestesamlingen for method chaining</returns>
        public static IServiceCollection LeggTilNorskSkattTjeneste(this IServiceCollection tjenester, Action<NorskSkattAlternativer> konfigurerAlternativer = null)
        {
            // Konfigurer alternativer
            tjenester.Configure<NorskSkattAlternativer>(alternativer =>
            {
                konfigurerAlternativer?.Invoke(alternativer);
                SettStandardAlternativer(alternativer);
            });

            // Registrer tjenester som singleton
            tjenester.AddSingleton<INorskSkatteTjeneste, NorskSkatteTjeneste>();

            return tjenester;
        }

        /// <summary>
        /// Registrerer ENK-skattetjenester og dens avhengigheter
        /// </summary>
        /// <param name="tjenester">Tjenestesamlingen</param>
        /// <param name="konfigurerEnkAlternativer">Valgfri konfigurasjon av ENK-alternativer</param>
        /// <returns>Tjenestesamlingen for method chaining</returns>
        public static IServiceCollection LeggTilNorskEnkSkattTjenester(
            this IServiceCollection tjenester,
            Action<EnkAlternativer> konfigurerEnkAlternativer = null)
        {
            // Konfigurer ENK-alternativer
            tjenester.Configure<NorskSkattAlternativer>(alternativer =>
            {
                konfigurerEnkAlternativer?.Invoke(alternativer.EnkInnstillinger);
                SettStandardEnkAlternativer(alternativer.EnkInnstillinger);
            });

            // Registrer ENK-tjenesten
            tjenester.AddSingleton<IEnkeltPersonForetak, EnkeltPersonForetak>();

            return tjenester;
        }

        /// <summary>
        /// Setter standardalternativer for skattebiblioteket
        /// </summary>
        private static void SettStandardAlternativer(NorskSkattAlternativer alternativer)
        {
            // Sett standard skatteår
            alternativer.Skatteår = SkatteKonstanter.StandardSkatteår.ToString();

            // Sett standard kommunale skattesatser hvis ikke allerede satt
            if (alternativer.KommunaleSkattesatser?.Count == 0)
            {
                alternativer.KommunaleSkattesatser["0301"] = 22.0m; // Oslo
                alternativer.KommunaleSkattesatser["4601"] = 22.0m; // Bergen
            }

            // Sett standard trygdeavgiftsalternativer
            alternativer.Trygdeavgift.Sats = SkatteKonstanter.Trygdeavgift.SatsArbeidstaker;
            alternativer.Trygdeavgift.NedreGrense = SkatteKonstanter.Trygdeavgift.NedreGrense;
            alternativer.Trygdeavgift.ØvreGrense = SkatteKonstanter.Trygdeavgift.ØvreGrense;

            // Sett standard MVA-alternativer
            alternativer.MvaInnstillinger.StandardSats = SkatteKonstanter.MvaSatser.Standard;
            alternativer.MvaInnstillinger.RedusertSats = SkatteKonstanter.MvaSatser.Redusert;
            alternativer.MvaInnstillinger.MatvareSats = SkatteKonstanter.MvaSatser.Matvarer;
        }

        /// <summary>
        /// Setter standardalternativer for ENK
        /// </summary>
        private static void SettStandardEnkAlternativer(EnkAlternativer alternativer)
        {
            alternativer.StandardFradragsSats = SkatteKonstanter.Minifradrag.Sats;
            alternativer.MaksStandardFradrag = SkatteKonstanter.Minifradrag.MaksFradrag;
            alternativer.TrygdeavgiftSats = SkatteKonstanter.Trygdeavgift.SatsSelvstendig;
            alternativer.MinsteTrygdeavgiftInntekt = SkatteKonstanter.Trygdeavgift.NedreGrense;
            alternativer.MaksTrygdeavgiftInntekt = SkatteKonstanter.Trygdeavgift.ØvreGrense;
            alternativer.MaksHjemmekontorFradrag = SkatteKonstanter.Fradragsgrenser.HjemmekontorMaks;
            alternativer.MaksBilFradragPerKm = SkatteKonstanter.Fradragsgrenser.BilFradragPerKm;
        }
    }
}
