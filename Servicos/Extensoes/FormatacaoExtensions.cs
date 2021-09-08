using System.Text.RegularExpressions;

namespace AnalisadorSSIS.Servicos.Extensoes
{
    internal static class FormatacaoExtensions
    {
        // Formata SQL de forma a deixar espacos entre os caracteres especiais ( e )
        public static string FormatarCaracteresSql(this string sql)
        {
            string sqlFormatado = "";
            string expressao = @"\(|\)";
            Match m = Regex.Match(sql, expressao, RegexOptions.IgnoreCase);

            int ultimoIndice = 0;
            while (m.Success)
            {
                sqlFormatado += (sql.Substring(ultimoIndice, m.Index - ultimoIndice) + string.Format(" {0} ", m.Value));
                ultimoIndice = m.Index + 1;
                m = m.NextMatch();
            }

            sqlFormatado += sql.Substring(ultimoIndice);
            return sqlFormatado;
        }

        // Formata conteudo dos elementos, de forma a refletir um sql
        public static string FormatarConteudoElemento(this string str, bool ehTabela = false)
        {
            if (!string.IsNullOrEmpty(str))
                str = string.Format("{0} ; ", str);

            return (ehTabela && !string.IsNullOrEmpty(str)) ? string.Format("TABLE {0}", str) : str;
        }
    }
}
