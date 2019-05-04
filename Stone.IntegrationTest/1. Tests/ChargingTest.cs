using Stone.Charging.Messages;
using Stone.Framework.Http.Abstractions;
using Stone.Framework.Result.Abstractions;
using Stone.IntegrationTest.DataProviders;
using Stone.IntegrationTest.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Stone.IntegrationTest.Tests
{
    public class ChargingTest
    {
        [Theory]
        [MemberData(nameof(ChargeDataProvider.GetValidCharge), MemberType = typeof(ChargeDataProvider))]
        public async Task RegisterCharge_ValidCharge_ReturnsTrue(ChargeMessage charge)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetChargeConnector();

            // Act
            IApplicationResult<bool> result = await connector.PostAsync<ChargeMessage, bool>(string.Empty, charge);

            // Assert
            Assert.True(result.Data);
        }

        [Theory]
        [MemberData(nameof(ChargeDataProvider.GetInvalidCpfCharge), MemberType = typeof(ChargeDataProvider))]
        public async Task RegisterCharge_InvalidCpfCharge_ReturnsFalse(ChargeMessage charge)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetChargeConnector();

            // Act
            IApplicationResult<bool?> result = await connector.PostAsync<ChargeMessage, bool?>(string.Empty, charge);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("cpf", result.Messages[0].ToLower());
        }

        [Theory]
        [MemberData(nameof(ChargeDataProvider.GetInvalidMaturityCharge), MemberType = typeof(ChargeDataProvider))]
        public async Task RegisterCharge_InvalidMaturityCharge_ReturnsFalse(ChargeMessage charge)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetChargeConnector();

            // Act
            IApplicationResult<bool?> result = await connector.PostAsync<ChargeMessage, bool?>(string.Empty, charge);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("maturity", result.Messages[0].ToLower());
        }

        [Theory]
        [MemberData(nameof(ChargeDataProvider.GetInvalidValueCharge), MemberType = typeof(ChargeDataProvider))]
        public async Task RegisterCharge_InvalidValueCharge_ReturnsFalse(ChargeMessage charge)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetChargeConnector();

            // Act
            IApplicationResult<bool?> result = await connector.PostAsync<ChargeMessage, bool?>(string.Empty, charge);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("value", result.Messages[0].ToLower());
        }

        [Theory]
        [MemberData(nameof(ChargeDataProvider.GetInvalidSearchFilter), MemberType = typeof(ChargeDataProvider))]
        public async Task GetCharge_InvalidSearchFilter_ReturnsFalse(ChargeSearchMessage search)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetChargeConnector();
            string query = $"?cpf={search.Cpf}&referenceMonth={search.ReferenceMonth}";

            // Act
            IApplicationResult<List<ChargeMessage>> result = await connector.GetAsync<List<ChargeMessage>>(query);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Theory]
        [MemberData(nameof(ChargeDataProvider.GetSearchByReferenceMonth), MemberType = typeof(ChargeDataProvider))]
        public async Task GetCharge_SearchFilterByReferenceMonth_ReturnsTrue(ChargeSearchMessage search, ChargeMessage charge)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetChargeConnector();
            await connector.PostAsync<ChargeMessage, bool>(string.Empty, charge);

            // Act
            IApplicationResult<List<ChargeMessage>> result = await connector.GetAsync<List<ChargeMessage>>(QueryStringHelper.GetChargeSearch(search));

            // Assert
            Assert.True(result.Data.Count > 0);
            Assert.NotNull(result.Data.FirstOrDefault(it => it.Cpf == charge.Cpf));
        }

        [Theory]
        [MemberData(nameof(ChargeDataProvider.GetSearchByCpf), MemberType = typeof(ChargeDataProvider))]
        public async Task GetCharge_SearchFilterByCpf_ReturnsTrue(ChargeSearchMessage search, ChargeMessage charge)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetChargeConnector();
            await connector.PostAsync<ChargeMessage, bool>(string.Empty, charge);

            // Act
            IApplicationResult<List<ChargeMessage>> result = await connector.GetAsync<List<ChargeMessage>>(QueryStringHelper.GetChargeSearch(search));

            // Assert
            Assert.True(result.Data.Count > 0);
            Assert.NotNull(result.Data.FirstOrDefault(it => it.Cpf == charge.Cpf));
        }
    }
}
