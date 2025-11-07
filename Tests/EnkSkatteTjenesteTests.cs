using Innovation_Research_Norwegian_VAT_Integration.Grensesnitt;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Foresporsel;
using Innovation_Research_Norwegian_VAT_Integration.Modeller.Konfigurasjoner;
using Innovation_Research_Norwegian_VAT_Integration.Tjenester;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Innovation_Research_Norwegian_VAT_Integration.Tests
{
    public class EnkSkattTjenesteTests
    {
        private readonly Mock<INorskSkatteTjeneste> _mockSkatteTjeneste;
        private readonly NorskSkattAlternativer _alternativer;
        private readonly EnkTjeneste _enkSkattTjeneste;

        public EnkSkattTjenesteTests()
        {
            _mockSkatteTjeneste = new Mock<INorskSkatteTjeneste>();
            _alternativer = new NorskSkattAlternativer();
            _enkSkattTjeneste = new EnkSkattTjeneste(_mockSkatteTjeneste.Object, _alternativer, Mock.Of<ILogger<EnkSkattTjeneste>>());
        }

        [Fact]
        public async Task BeregnEnkSkattAsync_WithValidRequest_ReturnsResult()
        {
            // Arrange
            var request = new EnkBeregningForesporsel
            {
                VirksomhetsInntekt = 500000m,
                VirksomhetsUtgifter = 100000m,
                Kommunenummer = "0301"
            };

            // Act
            var result = await _enkSkattTjeneste.BeregnEnkSkattAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Suksess);
        }

        [Fact]
        public async Task BeregnEnkSkattAsync_WithNegativeInntekt_ThrowsArgumentException()
        {
            // Arrange
            var request = new EnkBeregningForespørsel
            {
                VirksomhetsInntekt = -1000m
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _enkSkattTjeneste.BeregnEnkSkattAsync(request));
        }
    }
}
