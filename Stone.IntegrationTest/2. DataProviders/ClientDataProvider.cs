using Stone.Clients.Messages;
using Stone.Framework.Utils.Cpf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stone.IntegrationTest.DataProviders
{
    public static class ClientDataProvider
    {
        private static List<ClientMessage> clients = new List<ClientMessage>()
        {
            new ClientMessage() { Cpf = CpfGenerator.Generate(), Name = Guid.NewGuid().ToString(), State = "XX" },
            new ClientMessage() { Cpf = CpfGenerator.Generate(), Name = Guid.NewGuid().ToString(), State = "XX" },
            new ClientMessage() { Cpf = CpfGenerator.Generate(), Name = Guid.NewGuid().ToString(), State = "XX" },
            new ClientMessage() { Cpf = CpfGenerator.Generate(), Name = Guid.NewGuid().ToString(), State = "XX" },
            new ClientMessage() { Cpf = CpfGenerator.Generate(), Name = Guid.NewGuid().ToString(), State = "XX" },
            new ClientMessage() { Cpf = CpfGenerator.Generate(), Name = Guid.NewGuid().ToString(), State = "XX" },
            new ClientMessage() { Cpf = CpfGenerator.Generate(), Name = Guid.NewGuid().ToString(), State = "XX" },
        };

        public static IEnumerable<object[]> GetValidClient()
        {
            yield return new object[] { clients.First() };
        }

        public static IEnumerable<object[]> GetInvalidCpfClient()
        {
            ClientMessage client = clients[1];
            client.Cpf = client.Cpf.Insert(10, "1");

            yield return new object[] { client };
        }

        public static IEnumerable<object[]> GetInvalidNameClient()
        {
            ClientMessage client = clients[2];
            client.Name = string.Empty;

            yield return new object[] { client };
        }

        public static IEnumerable<object[]> GetInvalidStateClient()
        {
            ClientMessage client = clients[3];
            client.State = string.Empty;

            yield return new object[] { client };
        }

        public static IEnumerable<object[]> GetSameClient()
        {
            ClientMessage client = clients[4];
            yield return new object[] { client, client };
        }
    }
}
