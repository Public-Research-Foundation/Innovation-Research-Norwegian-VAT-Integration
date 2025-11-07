using Innovation_Research_Norwegian_VAT_Integration.Grensesnitt;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Respons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Innovation_Research_Norwegian_VAT_Integration.Tjenester
{
    /// <summary>
    /// Konkret implementasjon av norsk skatteberegningstjeneste med konfigurerbare verdier
    /// </summary>
    public class NorskSkatteTjeneste : INorskSkatteTjeneste
    {
        private readonly NorskSkattAlternativer _alternativer;
        private readonly ILogger<NorskSkatteTjeneste> _logger;

        public event EventHandler<SkatteBeregningHendelseArgs> FørSkatteberegning;
        public event EventHandler<SkatteBeregningHendelseArgs> EtterSkatteberegning;
        public event EventHandler<SkatteFeilHendelseArgs> BeregningsFeil;

        public NorskSkattAlternativer Alternativer => _alternativer;

        public NorskSkatteTjeneste(NorskSkattAlternativer alternativer, ILogger<NorskSkatteTjeneste> logger = null)
        {
            _alternativer = alternativer ?? throw new ArgumentNullException(nameof(alternativer));
            _logger = logger;
        }

        /// <summary>
        /// Beregner personlig skatt med konfigurerbare parametere
        /// </summary>
        public async Task<SkatteBeregningsResultat> BeregnSkattAsync(SkatteBeregningsForespørsel forespørsel)
        {
            try
            {
                if (forespørsel == null)
                    throw new ArgumentNullException(nameof(forespørsel));

                if (forespørsel.Inntekt < 0)
                    throw new ArgumentException("Inntekt kan ikke være negativ", nameof(forespørsel.Inntekt));

                var resultat = new SkatteBeregningsResultat { BruttoInntekt = forespørsel.Inntekt };

                PåFørSkatteberegning(new SkatteBeregningHendelseArgs
                {
                    Forespørsel = forespørsel,
                    Resultat = resultat
                });

                await BeregnSkattInterntAsync(forespørsel, resultat);

                PåEtterSkatteberegning(new SkatteBeregningHendelseArgs
                {
                    Forespørsel = forespørsel,
                    Resultat = resultat
                });

                return resultat;
            }
            catch (Exception ex)
            {
                PåBeregningsFeil(new SkatteFeilHendelseArgs
                {
                    Unntak = ex,
                    Operasjon = nameof(BeregnSkattAsync),
                    Kontekst = new Dictionary<string, object> { ["Forespørsel"] = forespørsel }
                });

                _logger?.LogError(ex, "Feil ved beregning av skatt for inntekt: {Inntekt}", forespørsel?.Inntekt);
                throw;
            }
        }

        /// <summary>
        /// Intern metode for skatteberegning med konfigurerbare parametere
        /// </summary>
        private async Task BeregnSkattInterntAsync(SkatteBeregningsForespørsel forespørsel, SkatteBeregningsResultat resultat)
        {
            await Task.Run(() =>
            {
                // Hent konfigurasjonsverdier
                var trygdeConfig = _alternativer.Trygdeavgift;
                var beregningsConfig = _alternativer.BeregningsKonfigurasjon;

                // Trinn 1: Beregn trygdeavgift med konfigurerbare grenser
                decimal trygdeavgiftGrunnlag = Math.Max(forespørsel.Inntekt, trygdeConfig.NedreGrense);
                trygdeavgiftGrunnlag = Math.Min(trygdeavgiftGrunnlag, trygdeConfig.ØvreGrense);

                decimal trygdeavgift = trygdeavgiftGrunnlag * (trygdeConfig.Sats / 100);
                resultat.SkattOppdeling["Trygdeavgift"] = trygdeavgift;

                // Trinn 2: Beregn kommunal skatt med konfigurerbare satser
                decimal kommunalSkattSats = beregningsConfig.Satser.StandardSkatteprosent;
                if (_alternativer.KommunaleSkattesatser.ContainsKey(forespørsel.Kommunenummer))
                {
                    kommunalSkattSats = _alternativer.KommunaleSkattesatser[forespørsel.Kommunenummer];
                }

                decimal kommunalSkatt = forespørsel.Inntekt * (kommunalSkattSats / 100);
                resultat.SkattOppdeling["KommunalSkatt"] = kommunalSkatt;

                // Trinn 3: Beregn total skatt
                resultat.TotalSkatt = trygdeavgift + kommunalSkatt;
                resultat.NettoInntekt = forespørsel.Inntekt - resultat.TotalSkatt;
                resultat.Skattesats = (resultat.TotalSkatt / forespørsel.Inntekt) * 100;
                resultat.Suksess = true;
                resultat.Melding = "Skatteberegning fullført";
            });
        }

        /// <summary>
        /// Beregner trygdeavgift med konfigurerbare parametere
        /// </summary>
        public async Task<TrygdeAvgiftResultat> BeregnTrygdeAvgiftAsync(TrygdeAvgiftForespørsel forespørsel)
        {
            return await Task.Run(() =>
            {
                var trygdeConfig = _alternativer.Trygdeavgift;

                // Beregn trygdeavgift med konfigurerbare grenser
                decimal grunnlag = Math.Max(forespørsel.Inntekt, trygdeConfig.NedreGrense);
                grunnlag = Math.Min(grunnlag, trygdeConfig.ØvreGrense);

                decimal trygdeavgift = grunnlag * (trygdeConfig.Sats / 100);

                return new TrygdeAvgiftResultat
                {
                    Inntekt = forespørsel.Inntekt,
                    Trygdeavgift = trygdeavgift,
                    BeregnetGrunnlag = grunnlag,
                    Sats = trygdeConfig.Sats
                };
            });
        }

        /// <summary>
        /// Beregner MVA med konfigurerbare satser
        /// </summary>
        public async Task<MvaBeregningsResultat> BeregnMvaAsync(MvaBeregningsForespørsel forespørsel)
        {
            return await Task.Run(() =>
            {
                var mvaConfig = _alternativer.MvaInnstillinger;
                decimal mvaSats = mvaConfig.StandardSats;

                // Velg riktig MVA-sats basert på type fra konfigurasjon
                switch (forespørsel.MvaType)
                {
                    case MvaType.Redusert:
                        mvaSats = mvaConfig.RedusertSats;
                        break;
                    case MvaType.Matvarer:
                        mvaSats = mvaConfig.MatvareSats;
                        break;
                    case MvaType.Standard:
                    default:
                        mvaSats = mvaConfig.StandardSats;
                        break;
                }

                decimal mvaBeløp;
                decimal nettoBeløp;
                decimal bruttoBeløp;

                if (forespørsel.InkludererMva)
                {
                    bruttoBeløp = forespørsel.Beløp;
                    nettoBeløp = bruttoBeløp / (1 + (mvaSats / 100));
                    mvaBeløp = bruttoBeløp - nettoBeløp;
                }
                else
                {
                    nettoBeløp = forespørsel.Beløp;
                    mvaBeløp = nettoBeløp * (mvaSats / 100);
                    bruttoBeløp = nettoBeløp + mvaBeløp;
                }

                return new MvaBeregningsResultat
                {
                    NettoBeløp = nettoBeløp,
                    MvaBeløp = mvaBeløp,
                    BruttoBeløp = bruttoBeløp,
                    MvaSats = mvaSats,
                    MvaType = forespørsel.MvaType
                };
            });
        }

        // Hendelsesmetoder (samme som før)
        protected virtual void PåFørSkatteberegning(SkatteBeregningHendelseArgs e)
        {
            FørSkatteberegning?.Invoke(this, e);
        }

        protected virtual void PåEtterSkatteberegning(SkatteBeregningHendelseArgs e)
        {
            EtterSkatteberegning?.Invoke(this, e);
        }

        protected virtual void PåBeregningsFeil(SkatteFeilHendelseArgs e)
        {
            BeregningsFeil?.Invoke(this, e);
            _logger?.LogError(e.Unntak, "Skatteberegningsfeil i {Operasjon}", e.Operasjon);
        }
    }
}
