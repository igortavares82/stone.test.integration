using Stone.Clients.Messages;
using Stone.Framework.Http.Abstractions;
using Stone.Framework.Result.Abstractions;
using Stone.IntegrationTest.DataProviders;
using Stone.IntegrationTest.Helpers;
using Stone.Rate.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Stone.IntegrationTest.Tests
{
    public class RatingTest
    {
        [Theory]
        [MemberData(nameof(RatingDataProvider.GetValidClients), MemberType = typeof(RatingDataProvider))]
        public void Rate_CallRateService_ReturnsTrue(List<ClientMessage> clients)
        {
            // Arrange
            IHttpConnector clientConnector = HttpConnectorHelper.GetClientConnector();
            IHttpConnector rateConnector = HttpConnectorHelper.GetRateConnector();

            string[] cpfs = clients.Select(it => it.Cpf).ToArray();
            decimal[] values = clients.Select(it => ConvertCpfToValue(it.Cpf)).ToArray();

            clients.ForEach(it => clientConnector.PostAsync<ClientMessage, bool?>(string.Empty, it));
            Thread.Sleep(2000);

            // Act
            IApplicationResult<List<RateMessage>> result = rateConnector.GetAsync<List<RateMessage>>(string.Empty).Result;

            // Assert
            Assert.Equal(clients.Count, result.Data.Where(it => cpfs.Contains(it.Cpf)).Count());
            Assert.Equal(clients.Count, result.Data.Where(it => values.Contains(it.Value)).Count());
        }

        private decimal ConvertCpfToValue(string cpf)
        {
            List<char> values = new List<char>();

            values.AddRange(cpf.Take(2));
            values.AddRange(cpf.Skip(9).Take(2));

            return Convert.ToDecimal(string.Join(string.Empty, values));
        }
    }
}
