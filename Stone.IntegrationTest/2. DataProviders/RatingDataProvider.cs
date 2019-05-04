using Stone.Clients.Messages;
using Stone.Framework.Utils.Cpf;
using System;
using System.Collections.Generic;

namespace Stone.IntegrationTest.DataProviders
{
    public class RatingDataProvider
    {
        private static List<ClientMessage> clients = new List<ClientMessage>()
        {
            new ClientMessage() { Cpf = CpfGenerator.Generate(), Name = Guid.NewGuid().ToString(), State = "XX" },
            new ClientMessage() { Cpf = CpfGenerator.Generate(), Name = Guid.NewGuid().ToString(), State = "XX" },
            new ClientMessage() { Cpf = CpfGenerator.Generate(), Name = Guid.NewGuid().ToString(), State = "XX" },
        };

        public static IEnumerable<object[]> GetValidClients()
        {
            yield return new object[] { clients };
        }
    }
}
