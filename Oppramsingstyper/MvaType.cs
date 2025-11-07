using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Oppramsingstyper
{
    /// <summary>
    /// Type merverdiavgift (MVA) for beregninger
    /// Bestemmer hvilken sats som skal brukes for MVA-beregning
    /// </summary>
    public enum MvaType
    {
        /// <summary>
        /// Standard MVA sats på 25%
        /// Gjelder for de fleste varer og tjenester
        /// </summary>
        Standard,

        /// <summary>
        /// Redusert MVA sats på 15%
        /// Gjelder for f.eks. næringsmidler, persontransport
        /// </summary>
        Redusert,

        /// <summary>
        /// MVA sats for matvarer på 15%
        /// Spesifikk for matvarer ifølge matvareforskriften
        /// </summary>
        Matvarer,

        /// <summary>
        /// MVA sats for fisk og fiskeprodukter på 15%
        /// Spesifikk for fisk, skalldyr og fiskeprodukter
        /// </summary>
        Fisk,

        /// <summary>
        /// MVA sats for persontransport på 15%
        /// Gjelder for buss, tog, taxi, flybilletter
        /// </summary>
        Persontransport,

        /// <summary>
        /// MVA sats for overnatting på 15%
        /// Gjelder for hotell, camping, overnatting
        /// </summary>
        Overnatting,

        /// <summary>
        /// Lav MVA sats på 12%
        /// Gjelder for kinobilletter, kulturarrangementer
        /// </summary>
        Lav
    }
}
