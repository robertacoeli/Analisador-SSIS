using AnalisadorSSIS.Modelo;
using AnalisadorSSIS.Servicos.Extensoes;
using System.Xml.Linq;

namespace AnalisadorSSIS.Servicos
{
    internal static class ComumServicos
    {
        public static Conexao ObterConexao(this XElement elemento)
        {
            Conexao conexao = new Conexao();
            conexao.Nome = elemento.ObterNome();
            conexao.Id = elemento.ObterId();
            conexao.TipoConexao = elemento.ObterTipoConexao();
            conexao.ConnectionString = elemento.ObterConnectionManager().ObterConnectionString();

            return conexao;
        }
    }
}
