using Microsoft.Extensions.Logging;
using Innovation_Research_Norwegian_VAT_Integration.Grensesnitt;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Hendelser;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Konfigurasjoner;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel.NorskSkatteBibliotek.Models.Requests;

namespace Innovation_Research_Norwegian_VAT_Integration.Tjenester
{

    /// <summary>
    /// Konkret implementasjon av ENK-skattetjeneste med fullstendig konfigurerbare beregningsparametere
    /// Håndterer alle skatteberegninger for Enkeltpersonforetak (ENK) i henhold til norsk skattelovgivning
    /// </summary>
    public class EnkTjeneste : IEnkeltPersonForetak
    {
        private readonly INorskSkatteTjeneste _skatteTjeneste;
        private readonly NorskSkattAlternativer _alternativer;
        private readonly ILogger<EnkTjeneste> _logger;

        /// <summary>
        /// Hendelse som utløses før ENK-beregning starter
        /// </summary>
        public event EventHandler<EnkBeregningHendelseArgs> FørEnkBeregning;

        /// <summary>
        /// Hendelse som utløses etter at ENK-beregning er fullført
        /// </summary>
        public event EventHandler<EnkBeregningHendelseArgs> EtterEnkBeregning;

        /// <summary>
        /// Hendelse som utløses ved validering av utgifter
        /// </summary>
        public event EventHandler<EnkUtgiftValideringHendelseArgs> UtgiftValidert;

        /// <summary>
        /// Konstruktør for ENK-skattetjeneste med dependency injection
        /// </summary>
        /// <param name="skatteTjeneste">Grunnleggende skattetjeneste for fellesberegninger</param>
        /// <param name="alternativer">Konfigurasjon for ENK-spesifikke innstillinger</param>
        /// <param name="logger">Valgfri logger for ENK-spesifikk logging</param>
        /// <exception cref="ArgumentNullException">Kastes hvis skatteTjeneste eller alternativer er null</exception>
        public EnkTjeneste(INorskSkatteTjeneste skatteTjeneste, NorskSkattAlternativer alternativer, ILogger<EnkTjeneste> logger = null)
        {
            _skatteTjeneste = skatteTjeneste ?? throw new ArgumentNullException(nameof(skatteTjeneste));
            _alternativer = alternativer ?? throw new ArgumentNullException(nameof(alternativer));
            _logger = logger;
        }

        /// <summary>
        /// Beregner komplett skatt for ENK-virksomhet med konfigurerbare parametere
        /// Prosedyre:
        /// 1. Validerer input data
        /// 2. Beregner virksomhetsresultat med minifradrag
        /// 3. Beregner trygdeavgift for selvstendig
        /// 4. Beregner personlig skatt på virksomhetsinntekt
        /// 5. Genererer skatteråd og anbefalinger
        /// </summary>
        /// <param name="forespørsel">ENK-spesifikk data inkludert inntekter, utgifter og kontekst</param>
        /// <returns>Detaljert skatteresultat med ENK-spesifikke beregninger og råd</returns>
        /// <exception cref="ArgumentNullException">Kastes hvis forespørsel er null</exception>
        public async Task<EnkBeregningResultat> BeregnEnkSkattAsync(EnkBeregningForesporsel forespørsel)
        {
            try
            {
                // Valider input parametere
                if (forespørsel == null)
                    throw new ArgumentNullException(nameof(forespørsel));

                if (forespørsel.VirksomhetsInntekt < 0)
                    throw new ArgumentException("Virksomhetsinntekt kan ikke være negativ", nameof(forespørsel.VirksomhetsInntekt));

                var resultat = new EnkBeregningResultat();

                // Utløs Før-beregning hendelse for overvåkning og logging
                PåFørEnkBeregning(new EnkBeregningHendelseArgs
                {
                    Forespørsel = forespørsel,
                    Resultat = resultat
                });

                // Trinn 1: Beregn virksomhetsresultat med minifradrag og utgifter
                await BeregnVirksomhetsResultatAsync(forespørsel, resultat);

                // Trinn 2: Beregn trygdeavgift for selvstendig næringsdrivende
                await BeregnTrygdeavgiftForSelvstendigAsync(forespørsel, resultat);

                // Trinn 3: Beregn personlig skatt på virksomhetsinntekt
                await BeregnPersonligSkattPåVirksomhetsinntektAsync(forespørsel, resultat);

                // Trinn 4: Generer skatteråd og anbefalinger basert på resultatet
                await GenererSkatterådAsync(forespørsel, resultat);

                resultat.Suksess = true;
                resultat.Melding = "ENK skatteberegning fullført";

                // Utløs Etter-beregning hendelse for oppfølging
                PåEtterEnkBeregning(new EnkBeregningHendelseArgs
                {
                    Forespørsel = forespørsel,
                    Resultat = resultat
                });

                return resultat;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Feil ved beregning av ENK-skatt for virksomhetsinntekt: {VirksomhetsInntekt}", forespørsel?.VirksomhetsInntekt);
                throw;
            }
        }

        /// <summary>
        /// Beregner optimal lønn/utbytte fordeling for skatteoptimalisering
        /// Prosedyre:
        /// 1. Analyserer virksomhetsresultatet
        /// 2. Beregner optimal lønn basert på konfigurerbare strategiparametere
        /// 3. Beregner resterende beløp for utbytte
        /// 4. Genererer anbefalinger for fordeling
        /// </summary>
        /// <param name="forespørsel">ENK-data for beregning av optimal lønn</param>
        /// <returns>Resultat med anbefalt lønn/utbytte fordeling og strategiråd</returns>
        public async Task<EnkBeregningResultat> BeregnOptimalLønnAsync(EnkBeregningForespørsel forespørsel)
        {
            return await Task.Run(() =>
            {
                var resultat = new EnkBeregningResultat();
                var optimalLønnConfig = _alternativer.BeregningsKonfigurasjon.OptimalLønnStrategi;
                var inntektsGrenser = _alternativer.BeregningsKonfigurasjon.InntektsGrenser;

                // Beregn optimal lønn basert på konfigurerbare strategiparametere
                decimal optimalLønn = BeregnOptimalLønnStrategi(
                    forespørsel.VirksomhetsResultat,
                    optimalLønnConfig,
                    inntektsGrenser);

                resultat.ForeslåttLønn = optimalLønn;
                resultat.ForeslåttUtbytte = Math.Max(0, forespørsel.VirksomhetsResultat - optimalLønn);
                resultat.Råd = new EnkSkatteråd
                {
                    EstimertOptimalLønn = optimalLønn,
                    AnbefalteHandlinger = GenererLønnAnbefalinger(optimalLønn, forespørsel.VirksomhetsResultat, optimalLønnConfig),
                    Risikonivå = BeregnRisikonivå(forespørsel.VirksomhetsResultat)
                };

                return resultat;
            });
        }

        /// <summary>
        /// Beregner trygdeavgift for selvstendig næringsdrivende
        /// Beregning: 11.4% på inntekt mellom 69 900 NOK og 750 000 NOK (2024)
        /// </summary>
        /// <param name="forespørsel">ENK-data for trygdeavgiftsberegning</param>
        /// <returns>Beregnet trygdeavgift for selvstendig næringsdrivende</returns>
        public async Task<decimal> BeregnTrygdeavgiftAsync(EnkBeregningForesporsel forespørsel)
        {
            return await Task.Run(() =>
            {
                var enkConfig = _alternativer.EnkInnstillinger;

                // Beregn trygdeavgiftsgrunnlag med konfigurerbare grenser
                decimal grunnlag = Math.Max(forespørsel.VirksomhetsInntekt, enkConfig.MinsteTrygdeavgiftInntekt);
                grunnlag = Math.Min(grunnlag, enkConfig.MaksTrygdeavgiftInntekt);

                // Beregn trygdeavgift med konfigurerbar sats
                return grunnlag * (enkConfig.TrygdeavgiftSats / 100);
            });
        }

        /// <summary>
        /// Validerer virksomhetsutgifter mot norske skatteregler og fradragsgrenser
        /// Prosedyre:
        /// 1. Validerer hjemmekontor fradrag mot maks grense
        /// 2. Validerer bilutgifter mot kilometergrenser
        /// 3. Sjekker andre utgifter for rimelighet
        /// 4. Returnerer detaljert valideringsresultat
        /// </summary>
        /// <param name="forespørsel">ENK-data med utgifter som skal valideres</param>
        /// <returns>Valideringsresultat med eventuelle advarsler og anbefalinger</returns>
        public async Task<VirksomhetsUtgiftValideringsResultat> ValiderVirksomhetsUtgifterAsync(EnkBeregningForespørsel forespørsel)
        {
            return await Task.Run(() =>
            {
                var resultat = new VirksomhetsUtgiftValideringsResultat();
                var fradragsGrenser = _alternativer.BeregningsKonfigurasjon.FradragsGrenser;

                // Valider hjemmekontor fradrag med konfigurerbar grense
                if (forespørsel.KontorUtgifter > 0)
                {
                    ValiderHjemmekontorUtgifter(forespørsel, resultat, fradragsGrenser);
                }

                // Valider bilutgifter med konfigurerbart kilometergrense
                if (forespørsel.ReiseUtgifter > 0)
                {
                    ValiderReiseutgifter(forespørsel, resultat, fradragsGrenser);
                }

                // Valider andre utgifter for rimelighet
                ValiderAndreUtgifter(forespørsel, resultat);

                resultat.ErGyldig = resultat.Advarsler.Count == 0 && resultat.Feil.Count == 0;

                // Utløs utgift validering hendelse
                PåUtgiftValidert(new EnkUtgiftValideringHendelseArgs
                {
                    Forespørsel = forespørsel,
                    ValideringsResultat = resultat
                });

                return resultat;
            });
        }

        /// <summary>
        /// Henter maksimalt tillatte fradragsgrenser for spesifikk bransje og år
        /// Inkluderer både generelle og bransjespesifikke grenser
        /// </summary>
        /// <param name="bransje">Bransjekode for spesifikke fradragsregler</param>
        /// <param name="år">Skatteåret for gjeldende regler</param>
        /// <returns>Dictionary med fradragstyper og deres grenser</returns>
        public async Task<Dictionary<string, decimal>> HentMaksFradragsGrenserAsync(string bransje, int år)
        {
            return await Task.Run(() =>
            {
                var grenser = new Dictionary<string, decimal>();
                var fradragsGrenser = _alternativer.BeregningsKonfigurasjon.FradragsGrenser;

                // Legg til generelle fradragsgrenser
                grenser["Hjemmekontor"] = fradragsGrenser.MaksHjemmekontorFradrag;
                grenser["BilPerKm"] = fradragsGrenser.BilFradragPerKm;

                // Legg til bransjespesifikke grenser hvis tilgjengelig
                if (_alternativer.EnkInnstillinger.BransjeSpesifikkeSatser.ContainsKey(bransje))
                {
                    grenser["BransjeSpesifikk"] = _alternativer.EnkInnstillinger.BransjeSpesifikkeSatser[bransje];
                }

                return grenser;
            });
        }

        /// <summary>
        /// Foreslår skatteplanleggingsstrategi basert på ENK-data og konfigurasjon
        /// Inkluderer anbefalinger for:
        /// - Investeringer og sparing
        /// - Utgiftsplanlegging
        /// - Skatteoptimaliseringstiltak
        /// </summary>
        /// <param name="forespørsel">ENK-data for skatteplanlegging</param>
        /// <returns>Skatteplanleggingsstrategi med anbefalte handlinger og tidsrammer</returns>
        public async Task<SkattePlanleggingsResultat> ForeslåSkatteplanleggingsStrategiAsync(EnkBeregningForespørsel forespørsel)
        {
            return await Task.Run(() =>
            {
                var resultat = new SkattePlanleggingsResultat();
                var inntektsGrenser = _alternativer.BeregningsKonfigurasjon.InntektsGrenser;

                // Analyser virksomhetens størrelse for tilpassede anbefalinger
                if (forespørsel.VirksomhetsInntekt < inntektsGrenser.LavInntektGrense)
                {
                    resultat.Strategier = new[]
                    {
                        "Fokuser på å dokumentere alle utgifter for maksimalt fradrag",
                        "Vurder å ta ut mesteparten som lønn for å utnytte personfradrag",
                        "Planlegg for skatteforhåndsinnbetaling basert på forventet inntekt"
                };
                    resultat.AnbefaltTidsramme = "Kvartalsvis oppfølging";
                }
                else if (forespørsel.VirksomhetsInntekt <= inntektsGrenser.MiddelsInntektGrense)
                {
                    resultat.Strategier = new[]
                    {
                        "Vurder investeringer i utstyr for å øke avskrivninger",
                        "Planlegg lønn/utbytte fordeling for skatteoptimalisering",
                        "Vurder pensjonssparing som selvstendig næringsdrivende"
                };
                    resultat.AnbefaltTidsramme = "Månedlig oppfølging";
                }
                else
                {
                    resultat.Strategier = new[]
                    {
                        "Vurder omdanning til AS for bedre skatteplanlegging",
                        "Planlegg for utbytte til aksjonærer",
                        "Vurder investeringer i andre virksomheter for diversifisering"
                };
                    resultat.AnbefaltTidsramme = "Kontinuerlig oppfølging med skatterådgiver";
                }

                resultat.EstimertBesparelse = BeregnEstimertSkattebesparelse(forespørsel);
                resultat.RisikoVurdering = "Middels";

                return resultat;
            });
        }

        /// <summary>
        /// Beregner estimert skatteforhåndsinnbetaling for selvstendig næringsdrivende
        /// Basert på forventet inntekt, tidligere års resultater og konfigurerbare parametere
        /// </summary>
        /// <param name="forespørsel">ENK-data for inntektsestimering</param>
        /// <returns>Estimert beløp for skatteforhåndsinnbetaling</returns>
        public async Task<decimal> BeregnEstimertSkatteForhåndsInnbetalingAsync(EnkBeregningForespørsel forespørsel)
        {
            return await Task.Run(() =>
            {
                // Beregn basert på forventet inntekt og tidligere resultater
                decimal estimertInntekt = forespørsel.VirksomhetsInntekt;

                // Juster basert på tidligere års resultater hvis tilgjengelig
                if (forespørsel.TidligereÅrsTap > 0)
                {
                    estimertInntekt = Math.Max(0, estimertInntekt - forespørsel.TidligereÅrsTap * 0.5m);
                }

                // Beregn estimert skatt (forenklet beregning)
                var skatteForespørsel = new SkatteBeregningsForespørsel
                {
                    Inntekt = estimertInntekt,
                    Kommunenummer = forespørsel.Kommunenummer
                };

                // Estimert skatt er ca 30% av inntekten for ENK (inkl trygdeavgift)
                decimal estimertSkatt = estimertInntekt * 0.30m;

                // Forhåndsinnbetaling er typisk 50% av estimert skatt
                return estimertSkatt * 0.5m;
            });
        }

        #region Private Hjelpemetoder

        /// <summary>
        /// Beregner virksomhetsresultat med konfigurerbare minifradrag og tapsoverføringsregler
        /// </summary>
        private async Task BeregnVirksomhetsResultatAsync(EnkBeregningForesporsel forespørsel, EnkBeregningResultat resultat)
        {
            await Task.Run(() =>
            {
                var enkConfig = _alternativer.EnkInnstillinger;

                // Beregn totale utgifter
                resultat.TotaleVirksomhetsUtgifter =
                    forespørsel.VirksomhetsUtgifter +
                    forespørsel.Lønnskostnader +
                    forespørsel.Avskrivninger +
                    forespørsel.KontorUtgifter +
                    forespørsel.ReiseUtgifter +
                    forespørsel.UtstyrUtgifter +
                    forespørsel.AndreVirksomhetsUtgifter;

                // Beregn minifradrag med konfigurerbare parametere
                decimal minifradrag = BeregnMinifradrag(forespørsel.VirksomhetsInntekt, enkConfig);

                // Beregn skattepliktig virksomhetsinntekt
                resultat.VirksomhetsResultat = forespørsel.VirksomhetsInntekt - resultat.TotaleVirksomhetsUtgifter - minifradrag;

                // Håndter tapsoverføring med konfigurerbare regler
                if (forespørsel.TidligereÅrsTap > 0 && enkConfig.TillatTapOverførsel)
                {
                    resultat.VirksomhetsResultat = Math.Max(0, resultat.VirksomhetsResultat - forespørsel.TidligereÅrsTap);
                }

                resultat.SkattepliktigVirksomhetsInntekt = resultat.VirksomhetsResultat;
            });
        }

        /// <summary>
        /// Beregner minifradrag med konfigurerbare satser og grenser
        /// </summary>
        private decimal BeregnMinifradrag(decimal virksomhetsInntekt, EnkAlternativer enkConfig)
        {
            // Bruk konfigurerbare verdier for minifradrag beregning
            if (virksomhetsInntekt <= SkatteKonstanter.Minifradrag.Inntektsgrense)
            {
                return Math.Min(virksomhetsInntekt * enkConfig.StandardFradragsSats, enkConfig.MaksStandardFradrag);
            }

            return enkConfig.MaksStandardFradrag;
        }

        /// <summary>
        /// Beregner trygdeavgift for selvstendig med konfigurerbare satser og grenser
        /// </summary>
        private async Task BeregnTrygdeavgiftForSelvstendigAsync(EnkBeregningForesporsel forespørsel, EnkBeregningResultat resultat)
        {
            await Task.Run(() =>
            {
                var enkConfig = _alternativer.EnkInnstillinger;

                // Beregn trygdeavgift med konfigurerbare grenser
                decimal trygdeavgiftGrunnlag = Math.Max(resultat.SkattepliktigVirksomhetsInntekt, enkConfig.MinsteTrygdeavgiftInntekt);
                trygdeavgiftGrunnlag = Math.Min(trygdeavgiftGrunnlag, enkConfig.MaksTrygdeavgiftInntekt);

                // Beregn trygdeavgift med konfigurerbar sats
                resultat.Trygdeavgift = trygdeavgiftGrunnlag * (enkConfig.TrygdeavgiftSats / 100);
                resultat.SelvstendigNæringsdrivendesSkatt = resultat.Trygdeavgift;

                resultat.SkattOppdeling["TrygdeavgiftSelvstendig"] = resultat.Trygdeavgift;
            });
        }

        /// <summary>
        /// Beregner personlig skatt på virksomhetsinntekt ved hjelp av grunnleggende skattetjeneste
        /// </summary>
        private async Task BeregnPersonligSkattPåVirksomhetsinntektAsync(EnkBeregningForesporsel forespørsel, EnkBeregningResultat resultat)
        {
            var skatteForespørsel = new SkatteBeregningsForesporsel
            {
                Inntekt = resultat.SkattepliktigVirksomhetsInntekt + forespørsel.LønnFraVirksomhet,
                Kommunenummer = forespørsel.Kommunenummer,
                Alder = forespørsel.Alder,
                Tilleggsparametre = forespørsel.Tilleggsparametre
            };

            var personligSkattResultat = await _skatteTjeneste.BeregnSkattAsync(skatteForespørsel);

            // Kombiner skatter: personskatt + trygdeavgift for selvstendig
            resultat.TotalSkatt = personligSkattResultat.TotalSkatt + resultat.Trygdeavgift;
            resultat.NettoInntekt = resultat.SkattepliktigVirksomhetsInntekt - resultat.TotalSkatt;
            resultat.Skattesats = (resultat.TotalSkatt / resultat.SkattepliktigVirksomhetsInntekt) * 100;

            // Flett sammen skatteoppdeling for komplett oversikt
            foreach (var oppdeling in personligSkattResultat.SkattOppdeling)
            {
                resultat.SkattOppdeling[oppdeling.Key] = oppdeling.Value;
            }
        }

        /// <summary>
        /// Beregner optimal lønn basert på konfigurerbare strategiparametere
        /// </summary>
        private decimal BeregnOptimalLønnStrategi(decimal virksomhetsResultat, OptimalLønnStrategi optimalLønnConfig, InntektsGrenser inntektsGrenser)
        {
            var satser = _alternativer.BeregningsKonfigurasjon.Satser;

            // Bruk konfigurerbare grenser og satser for optimal lønn beregning
            if (virksomhetsResultat <= inntektsGrenser.LavInntektGrense)
            {
                return virksomhetsResultat * satser.LavInntektLønnProsent;
            }
            else if (virksomhetsResultat <= inntektsGrenser.MiddelsInntektGrense)
            {
                return optimalLønnConfig.MiddelsInntektLønn;
            }
            else
            {
                return optimalLønnConfig.HøyInntektLønn;
            }
        }

        /// <summary>
        /// Genererer anbefalinger basert på konfigurerbare parametere og beregningsresultater
        /// </summary>
        private string[] GenererLønnAnbefalinger(decimal optimalLønn, decimal virksomhetsResultat, OptimalLønnStrategi optimalLønnConfig)
        {
            var anbefalinger = new List<string>();
            var inntektsGrenser = _alternativer.BeregningsKonfigurasjon.InntektsGrenser;

            // Bruk konfigurerbare grenser for anbefalinger
            if (virksomhetsResultat < inntektsGrenser.LavInntektGrense / 2)
            {
                anbefalinger.Add("Vurder å ta mesteparten av overskuddet som lønn på grunn av lavere skatteklasser");
            }
            else if (virksomhetsResultat > inntektsGrenser.MiddelsInntektGrense)
            {
                anbefalinger.Add("Vurder å etablere AS for bedre skatteoptimalisering ved høy inntekt");
                anbefalinger.Add("Maksimer lønn opp til trygdeavgiftsgrense for å sikre ytelser");
            }

            // Bruk konfigurerbar minimumslønn for anbefalinger
            if (optimalLønn < optimalLønnConfig.MinimumLønnForTrygdeytelser)
            {
                anbefalinger.Add($"Øk lønn til minst {optimalLønnConfig.MinimumLønnForTrygdeytelser:N0} NOK for trygdeytelser");
            }

            anbefalinger.Add("Konsulter med regnskapsfører for personlig rådgivning basert på din situasjon");

            return anbefalinger.ToArray();
        }

        /// <summary>
        /// Beregner risikonivå basert på virksomhetsresultat
        /// </summary>
        private string BeregnRisikonivå(decimal virksomhetsResultat)
        {
            var inntektsGrenser = _alternativer.BeregningsKonfigurasjon.InntektsGrenser;

            if (virksomhetsResultat < inntektsGrenser.LavInntektGrense)
                return "Lav";
            else if (virksomhetsResultat <= inntektsGrenser.MiddelsInntektGrense)
                return "Middels";
            else
                return "Høy";
        }

        /// <summary>
        /// Validerer hjemmekontor utgifter mot konfigurerbare grenser
        /// </summary>
        private void ValiderHjemmekontorUtgifter(EnkBeregningForesporsel forespørsel, VirksomhetsUtgiftValideringsResultat resultat, FradragsGrenser fradragsGrenser)
        {
            var maksKontor = fradragsGrenser.MaksHjemmekontorFradrag;
            var erGyldig = forespørsel.KontorUtgifter <= maksKontor;

            resultat.UtgiftsKategorier.Add(new VirksomhetsUtgiftKategori
            {
                Kategori = "Hjemmekontor",
                Beløp = forespørsel.KontorUtgifter,
                MaksTillatt = maksKontor,
                ErInnenforGrense = erGyldig
            });

            if (!erGyldig)
            {
                resultat.Advarsler.Add($"Hjemmekontor fradrag overstiger maks tillatt: {maksKontor} NOK. Skattetaten kan nekte fradrag over denne grensen.");
            }
        }

        /// <summary>
        /// Validerer reiseutgifter med konfigurerbare parametere
        /// </summary>
        private void ValiderReiseutgifter(EnkBeregningForesporsel forespørsel, VirksomhetsUtgiftValideringsResultat resultat, FradragsGrenser fradragsGrenser)
        {
            resultat.UtgiftsKategorier.Add(new VirksomhetsUtgiftKategori
            {
                Kategori = "Reise",
                Beløp = forespørsel.ReiseUtgifter,
                MaksTillatt = decimal.MaxValue, // Beregnes basert på faktisk kjørelengde
                ErInnenforGrense = true
            });

            // Sjekk for ekstremt høye reiseutgifter
            if (forespørsel.ReiseUtgifter > 50000m) // Konfigurerbar grense
            {
                resultat.Advarsler.Add("Reiseutgifter virker høye. Vær forberedt på å dokumentere alle reiser for Skattetaten.");
            }
        }

        /// <summary>
        /// Validerer andre utgifter for rimelighet og dokumentasjon
        /// </summary>
        private void ValiderAndreUtgifter(EnkBeregningForesporsel forespørsel, VirksomhetsUtgiftValideringsResultat resultat)
        {
            if (forespørsel.AndreVirksomhetsUtgifter > 0)
            {
                resultat.UtgiftsKategorier.Add(new VirksomhetsUtgiftKategori
                {
                    Kategori = "Andre utgifter",
                    Beløp = forespørsel.AndreVirksomhetsUtgifter,
                    MaksTillatt = decimal.MaxValue,
                    ErInnenforGrense = true
                });

                resultat.Anbefalinger.Add("Dokumenter alle andre utgifter med kvitteringer for skattemessig godkjenning.");
            }
        }

        /// <summary>
        /// Beregner estimert skattebesparelse basert på optimale strategier
        /// </summary>
        private decimal BeregnEstimertSkattebesparelse(EnkBeregningForesporsel forespørsel)
        {
            // Forenklet beregning - i praksis ville dette vært mer kompleks
            decimal basisSkatt = forespørsel.VirksomhetsInntekt * 0.30m; // 30% estimert skatt
            decimal optimalSkatt = basisSkatt * 0.85m; // 15% besparelse med optimal strategi

            return basisSkatt - optimalSkatt;
        }

        /// <summary>
        /// Genererer skatteråd basert på beregningsresultater og konfigurasjon
        /// </summary>
        private async Task GenererSkatterådAsync(EnkBeregningForesporsel forespørsel, EnkBeregningResultat resultat)
        {
            await Task.Run(() =>
            {
                var råd = new List<string>();
                var inntektsGrenser = _alternativer.BeregningsKonfigurasjon.InntektsGrenser;

                // Generer tilpassede råd basert på virksomhetsstørrelse
                if (resultat.VirksomhetsResultat < inntektsGrenser.LavInntektGrense)
                {
                    råd.Add("Fokuser på å øke inntekter fremfor skatteoptimalisering");
                    råd.Add("Dokumenter alle utgifter nøye for maksimalt fradrag");
                }
                else if (resultat.VirksomhetsResultat > inntektsGrenser.MiddelsInntektGrense)
                {
                    råd.Add("Vurder investeringer i utstyr for økte avskrivninger");
                    råd.Add("Planlegg lønn/utbytte fordeling for skatteoptimalisering");
                }

                råd.Add("Konsulter med autorisert regnskapsfører for dine spesifikke forhold");

                resultat.Råd = new EnkSkatteråd
                {
                    AnbefalteHandlinger = råd.ToArray(),
                    Risikonivå = BeregnRisikonivå(resultat.VirksomhetsResultat),
                    EstimertOptimalLønn = resultat.ForeslåttLønn
                };
            });
        }

        #endregion

        #region Hendelsesmetoder

        /// <summary>
        /// Utløser FørEnkBeregning-hendelsen på en trådsikker måte
        /// </summary>
        protected virtual void PåFørEnkBeregning(EnkBeregningHendelseArgs e)
        {
            FørEnkBeregning?.Invoke(this, e);
        }

        /// <summary>
        /// Utløser EtterEnkBeregning-hendelsen på en trådsikker måte
        /// </summary>
        protected virtual void PåEtterEnkBeregning(EnkBeregningHendelseArgs e)
        {
            EtterEnkBeregning?.Invoke(this, e);
        }

        /// <summary>
        /// Utløser UtgiftValidert-hendelsen på en trådsikker måte
        /// </summary>
        protected virtual void PåUtgiftValidert(EnkUtgiftValideringHendelseArgs e)
        {
            UtgiftValidert?.Invoke(this, e);
        }

        Task<EnkBeregningResultat> IEnkeltPersonForetak.BeregnOptimalLønnAsync(EnkBeregningForesporsel forespørsel)
        {
            throw new NotImplementedException();
        }

        Task<VirksomhetsUtgiftValideringsResultat> IEnkeltPersonForetak.ValiderVirksomhetsUtgifterAsync(EnkBeregningForesporsel forespørsel)
        {
            throw new NotImplementedException();
        }

        Task<SkattePlanleggingsResultat> IEnkeltPersonForetak.ForeslåSkatteplanleggingsStrategiAsync(EnkBeregningForesporsel forespørsel)
        {
            throw new NotImplementedException();
        }

        Task<decimal> IEnkeltPersonForetak.BeregnEstimertSkatteForhåndsInnbetalingAsync(EnkBeregningForesporsel forespørsel)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
