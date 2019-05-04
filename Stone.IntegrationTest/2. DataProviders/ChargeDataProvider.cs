using Stone.Charging.Messages;
using Stone.Framework.Utils.Cpf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stone.IntegrationTest.DataProviders
{
    public static class ChargeDataProvider
    {
        private static List<ChargeMessage> charges = new List<ChargeMessage>()
        {
            new ChargeMessage(){ Cpf = CpfGenerator.Generate(), Maturity = DateTime.Now, Value = 50.00M },
            new ChargeMessage(){ Cpf = CpfGenerator.Generate(), Maturity = DateTime.Now, Value = 50.00M },
            new ChargeMessage(){ Cpf = CpfGenerator.Generate(), Maturity = DateTime.Now, Value = 50.00M },
            new ChargeMessage(){ Cpf = CpfGenerator.Generate(), Maturity = DateTime.Now, Value = 50.00M },
            new ChargeMessage(){ Cpf = CpfGenerator.Generate(), Maturity = DateTime.Now, Value = 50.00M },
            new ChargeMessage(){ Cpf = CpfGenerator.Generate(), Maturity = DateTime.Now, Value = 50.00M },
            new ChargeMessage(){ Cpf = CpfGenerator.Generate(), Maturity = DateTime.Now, Value = 50.00M }
        };

        private static List<ChargeSearchMessage> searchs = new List<ChargeSearchMessage>()
        {
            new ChargeSearchMessage(){ Cpf = CpfGenerator.Generate(), ReferenceMonth = (short?)DateTime.Now.Month },
            new ChargeSearchMessage(){ Cpf = CpfGenerator.Generate(), ReferenceMonth = (short?)DateTime.Now.Month },
            new ChargeSearchMessage(){ Cpf = CpfGenerator.Generate(), ReferenceMonth = (short?)DateTime.Now.Month },
        };


        public static IEnumerable<object[]> GetValidCharge()
        {
            yield return new object[] { charges.First() };
        }

        public static IEnumerable<object[]> GetInvalidCpfCharge()
        {
            ChargeMessage charge = charges[1];
            charge.Cpf = charge.Cpf.Insert(10, "1");

            yield return new object[] { charge };
        }

        public static IEnumerable<object[]> GetInvalidMaturityCharge()
        {
            ChargeMessage charge = charges[2];
            charge.Maturity = null;

            yield return new object[] { charge };
        }

        public static IEnumerable<object[]> GetInvalidValueCharge()
        {
            ChargeMessage charge = charges[3];
            charge.Value = null;

            yield return new object[] { charge };
        }

        public static IEnumerable<object[]> GetInvalidSearchFilter()
        {
            ChargeSearchMessage search = searchs.First();

            search.Cpf = null;
            search.ReferenceMonth = null;

            yield return new object[] { search };
        }

        public static IEnumerable<object[]> GetSearchByReferenceMonth()
        {
            ChargeMessage charge = charges[4];
            ChargeSearchMessage search = searchs[1];

            search.Cpf = null;

            yield return new object[] { search, charge };
        }

        public static IEnumerable<object[]> GetSearchByCpf()
        {
            ChargeMessage charge = charges[6];
            ChargeSearchMessage search = searchs[2];

            search.Cpf = charge.Cpf;
            search.ReferenceMonth = null;

            yield return new object[] { search, charge };
        }
    }
}
