using Stone.Clients.Messages;
using Stone.Framework.Utils.Cpf;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
