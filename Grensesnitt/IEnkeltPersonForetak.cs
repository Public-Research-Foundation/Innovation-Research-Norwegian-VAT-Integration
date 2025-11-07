using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Hendelser;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Innovation_Research_Norwegian_VAT_Integration.Grensesnitt
{
    /// <summary>
    /// Spesialisert grensesnitt for skatteberegninger for Enkeltpersonforetak (ENK)
    /// Tilbyr ENK-spesifikke funksjoner som minifradrag, trygdeavgift for selvstendig, og utgiftsvalidering
    /// </summary>
    public interface IEnkeltPersonForetak
    {
        /// <summary>
        /// Beregner komplett skatt for ENK-virksomhet
        /// Inkluderer minifradrag, trygdeavgift for selvstendig, og fradrag for utgifter
        /// </summary>
        /// <param name="forespørsel">ENK-spesifikk data inkludert inntekter og utgifter</param>
        /// <returns>Detaljert skatteresultat med ENK-spesifikke beregninger</returns>
        Task<EnkBeregningResultat> BeregnEnkSkattAsync(EnkBeregningForesporsel forespørsel);

        /// <summary>
        /// Beregner optimal fordeling mellom lønn og utbytte for skatteoptimalisering
        /// Tar hensyn til trygdeavgiftsgrenser og skatteklasser
        /// </summary>
        /// <param name="forespørsel">ENK-data for beregning av optimal lønn</param>
        /// <returns>Anbefaling for lønn/utbytte fordeling</returns>
        Task<EnkBeregningResultat> BeregnOptimalLønnAsync(EnkBeregningForesporsel forespørsel);

        /// <summary>
        /// Beregner trygdeavgift for selvstendig næringsdrivende (11.4% for 2024)
        /// </summary>
        /// <param name="forespørsel">ENK-data for trygdeavgiftsberegning</param>
        /// <returns>Beregnet trygdeavgift for selvstendig</returns>
        Task<decimal> BeregnTrygdeavgiftAsync(EnkBeregningForesporsel forespørsel);

        /// <summary>
        /// Validerer virksomhetsutgifter mot norske skatteregler og fradragsgrenser
        /// Sjekker blant annet hjemmekontor, bil, og reiseutgifter
        /// </summary>
        /// <param name="forespørsel">ENK-data med utgifter som skal valideres</param>
        /// <returns>Valideringsresultat med eventuelle advarsler</returns>
        Task<VirksomhetsUtgiftValideringsResultat> ValiderVirksomhetsUtgifterAsync(EnkBeregningForesporsel forespørsel);

        /// <summary>
        /// Henter maksimalt tillatte fradragsgrenser for spesifikk bransje og år
        /// </summary>
        /// <param name="bransje">Bransjekode for spesifikke fradragsregler</param>
        /// <param name="år">Skatteåret for gjeldede regler</param>
        /// <returns>Dictionary med fradragstyper og deres grenser</returns>
        Task<Dictionary<string, decimal>> HentMaksFradragsGrenserAsync(string bransje, int år);

        /// <summary>
        /// Foreslår skatteplanleggingsstrategi basert på ENK-data
        /// Inkluderer anbefalinger for investeringer, sparing og utgiftsplanlegging
        /// </summary>
        /// <param name="forespørsel">ENK-data for skatteplanlegging</param>
        /// <returns>Skatteplanleggingsstrategi med anbefalte handlinger</returns>
        Task<SkattePlanleggingsResultat> ForeslåSkatteplanleggingsStrategiAsync(EnkBeregningForesporsel forespørsel);

        /// <summary>
        /// Beregner estimert skatteforhåndsinnbetaling for selvstendig næringsdrivende
        /// Basert på forventet inntekt og tidligere års resultater
        /// </summary>
        /// <param name="forespørsel">ENK-data for inntektsestimering</param>
        /// <returns>Estimert beløp for skatteforhåndsinnbetaling</returns>
        Task<decimal> BeregnEstimertSkatteForhåndsInnbetalingAsync(EnkBeregningForesporsel forespørsel);

        // ENK-spesifikke hendelser

        /// <summary>
        /// Utløses før ENK-beregning starter
        /// Kan brukes til validering av ENK-spesifikk data
        /// </summary>
        event EventHandler<EnkBeregningHendelseArgs> FørEnkBeregning;

        /// <summary>
        /// Utløses etter at ENK-beregning er fullført
        /// Kan brukes til logging av ENK-spesifikke resultater
        /// </summary>
        event EventHandler<EnkBeregningHendelseArgs> EtterEnkBeregning;

        /// <summary>
        /// Utløses ved validering av utgifter
        /// Kan brukes til å logge utgiftsvalideringer
        /// </summary>
        event EventHandler<EnkUtgiftValideringHendelseArgs> UtgiftValidert;
    }
}
