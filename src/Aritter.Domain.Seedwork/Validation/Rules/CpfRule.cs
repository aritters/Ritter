﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Aritter.Domain.Seedwork.Validation.Rules
{
    public class CpfRule<T> : GenericRule<T, string>
    {
        private readonly Regex regex = new Regex(@"[0-9]{3}\.?[0-9]{3}\.?[0-9]{3}\-?[0-9]{2}", RegexOptions.Compiled);

        public CpfRule(Expression<Func<T, string>> expression) : base(expression)
        {
        }

        public CpfRule(Func<T, string> provider) : base(provider)
        {
        }

        public override bool Validate(Func<T> source)
        {
            return ValidaCpf(provider(source()));
        }

        public bool ValidaCpf(string cpf)
        {
            if (!regex.IsMatch(cpf))
            {
                return false;
            }

            string innerCpf = cpf.Trim().Replace(".", "").Replace("-", "");

            int[] mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCPF;
            string digito;
            int soma;
            int resto;

            if (innerCpf.Length != 11 || innerCpf.Distinct().Count() == 1)
            {
                return false;
            }

            tempCPF = innerCpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCPF[i].ToString()) * mt1[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito = resto.ToString();
            tempCPF = tempCPF + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCPF[i].ToString()) * mt2[i];
            }

            resto = soma % 11;

            resto = resto < 2 ? 0 : 11 - resto;
            digito = digito + resto.ToString();

            return innerCpf.EndsWith(digito);
        }
    }
}