using Stone.Clients.Messages;
using Stone.Framework.Http.Abstractions;
using Stone.Framework.Result.Abstractions;
using Stone.IntegrationTest.DataProviders;
using Stone.IntegrationTest.Helpers;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Stone.IntegrationTest.Tests
{
    public class ClientsTest
    {
        [Theory]
        [MemberData(nameof(ClientDataProvider.GetValidClient), MemberType = typeof(ClientDataProvider))]
        public async Task RegisterClient_ValidClient_ReturnsTrue(ClientMessage client)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetClientConnector();

            // Act
            IApplicationResult<bool> result = await connector.PostAsync<ClientMessage, bool>(string.Empty, client);

            // Assert
            Assert.True(result.Data);
        }

        [Theory]
        [MemberData(nameof(ClientDataProvider.GetInvalidCpfClient), MemberType = typeof(ClientDataProvider))]
        public async Task RegisterClient_InvalidCpfClient_ReturnsFalse(ClientMessage client)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetClientConnector();

            // Act
            IApplicationResult<bool?> result = await connector.PostAsync<ClientMessage, bool?>(string.Empty, client);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("cpf", result.Messages[0].ToLower());
        }

        [Theory]
        [MemberData(nameof(ClientDataProvider.GetInvalidNameClient), MemberType = typeof(ClientDataProvider))]
        public async Task RegisterClient_InvalidNameClient_ReturnsFalse(ClientMessage client)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetClientConnector();

            // Act
            IApplicationResult<bool?> result = await connector.PostAsync<ClientMessage, bool?>(string.Empty, client);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("name", result.Messages[0].ToLower());
        }

        [Theory]
        [MemberData(nameof(ClientDataProvider.GetInvalidStateClient), MemberType = typeof(ClientDataProvider))]
        public async Task RegisterClient_InvalidStateClient_ReturnsFalse(ClientMessage client)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetClientConnector();

            // Act
            IApplicationResult<bool?> result = await connector.PostAsync<ClientMessage, bool?>(string.Empty, client);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("state", result.Messages[0].ToLower());
        }

        [Theory]
        [MemberData(nameof(ClientDataProvider.GetSameClient), MemberType = typeof(ClientDataProvider))]
        public async Task RegisterClient_SameClient_ReturnsFalse(ClientMessage client, ClientMessage sameClient)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetClientConnector();
            await connector.PostAsync<ClientMessage, bool>(string.Empty, client);

            // Act
            IApplicationResult<bool> result = await connector.PostAsync<ClientMessage, bool>(string.Empty, sameClient);

            // Assert
            Assert.False(result.Data);
        }


        [Theory]
        [MemberData(nameof(ClientDataProvider.GetValidClient), MemberType = typeof(ClientDataProvider))]
        public async Task RegisterClient_SearchClient_ReturnsFalse(ClientMessage client)
        {
            // Arrange
            IHttpConnector connector = HttpConnectorHelper.GetClientConnector();
            await connector.PostAsync<ClientMessage, bool>(string.Empty, client);

            // Act
            IApplicationResult<ClientMessage> result = await connector.GetAsync<ClientMessage>($"/{client.Cpf}");

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(client.Cpf, result.Data.Cpf);
        }
    }
}
