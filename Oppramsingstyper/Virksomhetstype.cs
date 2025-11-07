using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Oppramsingstyper
{
    /// <summary>
    /// Type virksomhet for skatteberegning
    /// Påvirker hvilke regler og satser som brukes i beregninger
    /// </summary>
    public enum Virksomhetstype
    {
        /// <summary>
        /// Enkeltpersonforetak - vanligste form for selvstendig næringsdrivende
        /// Skattes som personskatt på overskudd fra virksomheten
        /// </summary>
        ENK,

        /// <summary>
        /// Aksjeselskap - eget rettssubjekt med aksjonærer
        /// Skattes med selskapsskatt og utbytteskatt
        /// </summary>
        AS,

        /// <summary>
        /// Ansvarlig selskap - personlig ansvar for deltakerne
        /// Skattes som personskatt på andel av overskudd
        /// </summary>
        ANS,

        /// <summary>
        /// Samvirkeforetak - eid av medlemmer/brukere
        /// Spesielle skatteregler for samvirkeforetak
        /// </summary>
        Samvirkeforetak,

        /// <summary>
        /// Stiftelse - ideell organisasjonsform
        /// Spesielle skatteregler for ideelle formål
        /// </summary>
        Stiftelse,

        /// <summary>
        /// Annet selskapstype - for spesielle tilfeller
        /// Brukes når ingen av de andre typene passer
        /// </summary>
        Annet
    }
}
