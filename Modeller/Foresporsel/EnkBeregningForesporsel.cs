using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel.NorskSkatteBibliotek.Models.Requests;
using Innovation_Research_Norwegian_VAT_Integration.Oppramsingstyper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel
{
    /// <summary>
    /// Forespørsel for beregning av skatt for Enkeltpersonforetak (ENK)
    /// </summary>
    public class EnkBeregningForesporsel : SkatteBeregningsForesporsel
    {
        /// <summary>
        /// Type virksomhet (ENK, AS, ANS, etc.)
        /// </summary>
        public Virksomhetstype Virksomhetstype { get; set; } = Virksomhetstype.ENK;

        /// <summary>
        /// Brutto inntekt fra virksomheten før fradrag
        /// Inkluderer alle inntekter fra salg, tjenester, etc.
        /// </summary>
        public decimal VirksomhetsInntekt { get; set; }

        /// <summary>
        /// Generelle virksomhetsutgifter som ikke er kategorisert
        /// Inkluderer diverse utgifter som ikke passer i andre kategorier
        /// </summary>
        public decimal VirksomhetsUtgifter { get; set; }

        /// <summary>
        /// Lønn tatt ut av virksomheten til eier
        /// Dette påvirker skattegrunnlaget og trygdeavgiftsberegningen
        /// </summary>
        public decimal LønnFraVirksomhet { get; set; }

        /// <summary>
        /// Angir om virksomheten har ansatte
        /// Påvirker regnskapsplikter og eventuelle fradrag
        /// </summary>
        public bool HarAnsatte { get; set; }

        /// <summary>
        /// Antall ansatte i virksomheten
        /// Brukes for beregning av arbeidsgiveravgift og andre ansatte-relaterte kostnader
        /// </summary>
        public int AntallAnsatte { get; set; }

        /// <summary>
        /// Lønnskostnader for ansatte (ikke eier)
        /// Inkluderer lønn, feriepenger, arbeidsgiveravgift, etc.
        /// </summary>
        public decimal Lønnskostnader { get; set; }

        /// <summary>
        /// Avskrivninger på anleggsmidler og inventar
        /// Beregnes i henhold til norske skatteregler for avskrivning
        /// </summary>
        public decimal Avskrivninger { get; set; }

        /// <summary>
        /// Kontorutgifter inkludert leie, strøm, internett, telefon, etc.
        /// Inkluderer også hjemmekontorutgifter med spesielle regler
        /// </summary>
        public decimal KontorUtgifter { get; set; }

        /// <summary>
        /// Reiseutgifter for virksomheten
        /// Inkluderer transport, overnatting, måltider under reise
        /// </summary>
        public decimal ReiseUtgifter { get; set; }

        /// <summary>
        /// Utstyr og inventarutgifter
        /// Inkluderer datamaskiner, møbler, verktøy, etc.
        /// </summary>
        public decimal UtstyrUtgifter { get; set; }

        /// <summary>
        /// Andre virksomhetsutgifter som ikke passer i andre kategorier
        /// F.eks. kurs, abonnementer, bankgebyrer, etc.
        /// </summary>
        public decimal AndreVirksomhetsUtgifter { get; set; }

        /// <summary>
        /// Tap fra tidligere år som kan overføres til dette året
        /// Ifølge norsk skattelov kan tap overføres i opptil 5 år
        /// </summary>
        public decimal TidligereÅrsTap { get; set; }

        /// <summary>
        /// Angir om dette er første år virksomheten driver
        /// Førsteårs virksomheter kan ha spesielle regler og fradrag
        /// </summary>
        public bool ErFørsteÅr { get; set; }

        /// <summary>
        /// Estimert privat bruk av virksomhetsmidler
        /// Inkluderer bil, hjemmekontor, telefon, etc. som brukes både privat og i virksomhet
        /// </summary>
        public decimal EstimertPrivatBruk { get; set; }

        /// <summary>
        /// Bransje eller næring for bransjespesifikke regler
        /// F.eks. "IT", "Konsulent", "Håndverk", "Detail"
        /// </summary>
        public string Bransje { get; set; }

        /// <summary>
        /// Antall kilometer kjørt i virksomhetsøyemed
        /// Brukes for beregning av bilfradrag
        /// </summary>
        public int AntallKilometer { get; set; }
    }

}
