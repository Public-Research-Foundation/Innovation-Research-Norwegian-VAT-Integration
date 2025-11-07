using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Konfigurasjoner
{
    /// <summary>
    /// Konfigurasjon for merverdiavgift (MVA) beregninger
    /// Definerer alle norske MVA-satser i henhold til gjeldende lovverk
    /// </summary>
    public class MvaAlternativer
    {
        /// <summary>
        /// Standard MVA sats for de fleste varer og tjenester (i prosent)
        /// 25% for de fleste kommersielle varer og tjenester
        /// </summary>
        public decimal StandardSats { get; set; } = 25.0m;

        /// <summary>
        /// Redusert MVA sats for spesielle varer og tjenester (i prosent)
        /// 15% for f.eks. næringsmidler, persontransport, overnatting
        /// </summary>
        public decimal RedusertSats { get; set; } = 15.0m;

        /// <summary>
        /// MVA sats for matvarer (i prosent)
        /// 15% for alle matvarer med noen unntak
        /// </summary>
        public decimal MatvareSats { get; set; } = 15.0m;

        /// <summary>
        /// Sats for fisk og fiskeprodukter (i prosent)
        /// 15% for fisk, skalldyr og andre fiskeprodukter
        /// </summary>
        public decimal FiskSats { get; set; } = 15.0m;

        /// <summary>
        /// Sats for persontransport (i prosent)
        /// 15% for buss, tog, taxi, flybilletter innenlands
        /// </summary>
        public decimal PersontransportSats { get; set; } = 15.0m;

        /// <summary>
        /// Sats for overnatting (i prosent)
        /// 15% for hotell, camping, og annen overnatting
        /// </summary>
        public decimal OvernattingSats { get; set; } = 15.0m;

        /// <summary>
        /// Lav sats for spesielle tjenester (i prosent)
        /// 12% for kinobilletter, fotballkamper, kulturarrangementer
        /// </summary>
        public decimal LavSats { get; set; } = 12.0m;

        /// <summary>
        /// Rundingsmetode for MVA-beløp
        /// "Opp": Rund opp til nærmeste hele krone
        /// "Ned": Rund ned til nærmeste hele krone  
        /// "Normal": Bruk standard avrundingsregler
        /// </summary>
        public string Rundingsmetode { get; set; } = "Normal";

        /// <summary>
        /// Antall desimaler for MVA-beløp i resultater
        /// 0: hele kroner, 2: kroner og ører
        /// </summary>
        public int AntallDesimaler { get; set; } = 2;
    }
}
