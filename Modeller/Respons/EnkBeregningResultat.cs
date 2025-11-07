using System;
using System.Collections.Generic;
using System.Text;

namespace Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons
{
    /// <summary>
    /// Resultat fra beregning av ENK-skatt
    /// Utvidet resultat med ENK-spesifikke beregninger og analyser
    /// </summary>
    public class EnkBeregningResultat : SkatteBeregningsResultat
    {
        /// <summary>
        /// Resultatet av virksomheten etter utgifter og minifradrag
        /// Beregnes som: VirksomhetsInntekt - TotaleVirksomhetsUtgifter - Minifradrag
        /// </summary>
        public decimal VirksomhetsResultat { get; set; }

        /// <summary>
        /// Totale virksomhetsutgifter inkludert alle kategorier
        /// Summen av alle utgifter fra ENK-forespørselen
        /// </summary>
        public decimal TotaleVirksomhetsUtgifter { get; set; }

        /// <summary>
        /// Skattepliktig virksomhetsinntekt etter fradrag og tapsoverføring
        /// Dette er grunnlaget for skatteberegning på virksomheten
        /// </summary>
        public decimal SkattepliktigVirksomhetsInntekt { get; set; }

        /// <summary>
        /// Trygdeavgift for selvstendig næringsdrivende (11.4%)
        /// Beregnes på inntekt mellom 69 900 NOK og 750 000 NOK
        /// </summary>
        public decimal Trygdeavgift { get; set; }

        /// <summary>
        /// Total skatt for selvstendig næringsdrivende
        /// Inkluderer både personskatt og trygdeavgift for selvstendig
        /// </summary>
        public decimal SelvstendigNæringsdrivendesSkatt { get; set; }

        /// <summary>
        /// Foreslått lønn for optimal skatteplanlegging
        /// Basert på strategi for balanse mellom lønn og utbytte
        /// </summary>
        public decimal ForeslåttLønn { get; set; }

        /// <summary>
        /// Foreslått utbytte for optimal skatteplanlegging
        /// Restbeløp etter at foreslått lønn er tatt ut
        /// </summary>
        public decimal ForeslåttUtbytte { get; set; }

        /// <summary>
        /// Detaljert oppdeling av utgifter i kategorier
        /// Inkluderer validering mot maksimumsgrenser
        /// </summary>
        public List<VirksomhetsUtgiftKategori> UtgiftsOppdeling { get; set; } = new List<VirksomhetsUtgiftKategori>();

        /// <summary>
        /// Råd og anbefalinger for skatteplanlegging og optimalisering
        /// Inkluderer både generelle og spesifikke anbefalinger
        /// </summary>
        public EnkSkatteråd Råd { get; set; }

        /// <summary>
        /// Beløp for minifradrag (standardfradrag) som ble anvendt
        /// 43% av inntekt opp til 200 000 NOK, maks 86 000 NOK
        /// </summary>
        public decimal AnvendtMinifradrag { get; set; }

        /// <summary>
        /// Eventuelt tap som ble overført fra tidligere år
        /// </summary>
        public decimal OverførtTap { get; set; }
    }

    /// <summary>
    /// Kategori for virksomhetsutgifter med valideringsinformasjon
    /// Brukes for detaljert rapportering og validering av utgifter
    /// </summary>
    public class VirksomhetsUtgiftKategori
    {
        /// <summary>
        /// Kategorinavn for utgiften
        /// F.eks. "Hjemmekontor", "Reise", "Utstyr"
        /// </summary>
        public string Kategori { get; set; }

        /// <summary>
        /// Beløp for kategorien i NOK
        /// </summary>
        public decimal Beløp { get; set; }

        /// <summary>
        /// Maksimalt tillatt beløp for kategorien ifølge skatteregler
        /// </summary>
        public decimal MaksTillatt { get; set; }

        /// <summary>
        /// Angir om beløpet er innenfor tillatt grense
        /// </summary>
        public bool ErInnenforGrense { get; set; }

        /// <summary>
        /// Prosentandel av maks grense som er brukt
        /// Beregnes som (Beløp / MaksTillatt) * 100
        /// </summary>
        public decimal ProsentAvMaksGrense { get; set; }

        /// <summary>
        /// Eventuelle advarsler for denne kategorien
        /// </summary>
        public string Advarsel { get; set; }
    }

    /// <summary>
    /// Råd og anbefalinger for ENK skatteplanlegging
    /// Basert på analyse av virksomhetens tall og norske skatteregler
    /// </summary>
    public class EnkSkatteråd
    {
        /// <summary>
        /// Anbefalte handlinger for skatteoptimalisering
        /// Konkrete tiltak eieren kan iverksette
        /// </summary>
        public string[] AnbefalteHandlinger { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Potensielle fradrag som kan utnyttes bedre
        /// Fradrag som virksomheten kvalifiserer for men kanskje ikke bruker
        /// </summary>
        public string[] PotensielleFradrag { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Estimert optimal lønn for skatteoptimalisering
        /// Basert på analyse av inntekt, utgifter og skatteklasser
        /// </summary>
        public decimal EstimertOptimalLønn { get; set; }

        /// <summary>
        /// Risikonivå for valgt strategi
        /// Lav: Konservativ strategi med lite risiko
        /// Middels: Balansert strategi
        /// Høy: Aggressiv strategi med høyere risiko
        /// </summary>
        public string Risikonivå { get; set; }

        /// <summary>
        /// Estimert skattebesparelse med anbefalte tiltak
        /// </summary>
        public decimal EstimertSkattebesparelse { get; set; }

        /// <summary>
        /// Anbefalt tidsramme for implementering
        /// F.eks. "Umiddelbart", "Innen 3 måneder", "Planlegg for neste år"
        /// </summary>
        public string AnbefaltTidsramme { get; set; }
    }
}
