using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel.NorskSkatteBibliotek.Models.Requests;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Hendelser;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Konfigurasjoner;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Innovation_Research_Norwegian_VAT_Integration.Grensesnitt
{
    /// <summary>
    /// Hovedgrensesnitt for norsk skatteberegningstjeneste
    /// Definerer grunnleggende operasjoner for alle skatteberegninger
    /// </summary>
    internal interface INorskSkatteTjeneste
    {
        /// <summary>
        /// Beregner personlig skatt basert på inntekt og andre parametere
        /// </summary>
        /// <param name="forespørsel">Inneholder all nødvendig data for skatteberegning</param>
        /// <returns>Resultat av skatteberegning inkludert netto og brutto beløp</returns>
        Task<SkatteBeregningsResultat> BeregnSkattAsync(SkatteBeregningsForesporsel forespørsel);

        /// <summary>
        /// Beregner trygdeavgift for arbeidstaker eller selvstendig næringsdrivende
        /// </summary>
        /// <param name="forespørsel">Inneholder inntektsdata for trygdeavgiftsberegning</param>
        /// <returns>Beregnet trygdeavgift og eventuelle grensejusteringer</returns>
        Task<TrygdeAvgiftResultat> BeregnTrygdeAvgiftAsync(TrygdeAvgiftForespørsel forespørsel);

        /// <summary>
        /// Beregner merverdiavgift (MVA) for varer og tjenester
        /// </summary>
        /// <param name="forespørsel">Inneholder beløp og MVA-type</param>
        /// <returns>Beregnet MVA-beløp og netto/beløp</returns>
        Task<MvaBeregningsResultat> BeregnMvaAsync(MvaBeregningsForesporsel forespørsel);

        // Hendelser for å overvåke beregningsprosessen

        /// <summary>
        /// Utløses før skatteberegning starter
        /// Kan brukes til validering eller logging
        /// </summary>
        event EventHandler<SkatteBeregningHendelseArgs> FørSkatteberegning;

        /// <summary>
        /// Utløses etter at skatteberegning er fullført
        /// Kan brukes til logging eller oppfølgingsberegninger
        /// </summary>
        event EventHandler<SkatteBeregningHendelseArgs> EtterSkatteberegning;

        /// <summary>
        /// Utløses ved feil i beregningsprosessen
        /// Kan brukes til feilhåndtering og alerting
        /// </summary>
        event EventHandler<SkatteFeilHendelseArgs> BeregningsFeil;

        /// <summary>
        /// Gir tilgang til gjeldende konfigurasjonsalternativer
        /// </summary>
        NorskSkattAlternativer Alternativer { get; }
    }
}
