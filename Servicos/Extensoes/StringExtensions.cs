using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AnalisadorSSIS.Servicos.Extensoes
{
    internal static class StringExtensions
    {
        internal static string RemoverAcentos(this string str)
        {
            var normalizedString = str.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        internal static string RemoverPontuacao(this string str)
        {
            return Regex.Replace(str, @"[^\w\d\s]", " ");
        }

        internal static string RemoverEscapeChars(this string str)
        {
            return Regex.Replace(str, @"[\n\a\b\t\r\v]", " ");
        }

        internal static string RemoverEspacosExtras(this string str)
        {
            return Regex.Replace(str, @"\s\s+", " ").Trim();
        }

        internal static string Formatar(this string str)
        {
            str = str.ToLower();
            str = str.RemoverEscapeChars();
            str = str.RemoverAcentos();
            str = str.RemoverPontuacao();
            str = str.RemoverEspacosExtras();
            return str;
        }
    }
}
