using Stone.Charging.Messages;
using Stone.Clients.Messages;

namespace Stone.IntegrationTest.Helpers
{
    public static class QueryStringHelper
    {
        public static string GetChargeSearch(ChargeSearchMessage search) => $"?cpf={search.Cpf}&referenceMonth={search.ReferenceMonth}";
    }
}
