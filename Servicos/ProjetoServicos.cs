using AnalisadorSSIS.Modelo;
using AnalisadorSSIS.Modelo.Enums;
using AnalisadorSSIS.Servicos.Extensoes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AnalisadorSSIS.Servicos
{
    internal static class ProjetoServicos
    {
        private static Conexao AnalisarConexao(string nomeArquivoConexao)
        {
            XElement arquivo = XElement.Load(nomeArquivoConexao);
            Conexao conexao = ComumServicos.ObterConexao(arquivo);
            conexao.NomeArquivo = nomeArquivoConexao;
            conexao.Escopo = Escopo.Project;

            return conexao;
        }

        private static IList<Conexao> ObterConexoes(string caminhoProjeto)
        {
            IList<Conexao> conexoes = new List<Conexao>();

            foreach (var nomeArquivoConexao in Directory.GetFiles(caminhoProjeto).Where(x => x.Contains(Config.extensaoArquivoConexao)))
            {
                Conexao conexao = AnalisarConexao(nomeArquivoConexao);
                conexoes.Add(conexao);
            }

            return conexoes;
        }

        private static IList<Variavel> ObterVariaveis(string caminhoProjeto)
        {
            IList<Variavel> variaveis = new List<Variavel>();

            foreach (var nomeArquivoParametro in Directory.GetFiles(caminhoProjeto).Where(x => x.Contains(Config.extensaoArquivoParametro)))
            {
                XElement arquivo = XElement.Load(nomeArquivoParametro);

                foreach (var v in arquivo.ObterParametros())
                {
                    Variavel variavel = new Variavel();
                    variavel.Nome = v.ObterNomeParametro();
                    variavel.Escopo = Escopo.Project;
                    variavel.Id = v.ObterIdParametroProjeto();
                    variavel.Conteudo = v.ObterValorParametroProjeto();
                    variaveis.Add(variavel);
                }
            }

            return variaveis;
        }

        public static Projeto Executar(Solucao solucao, int indiceProjeto)
        {
            Projeto projeto = new Projeto(solucao, solucao.Projetos[indiceProjeto]);
            projeto.Conexoes = ObterConexoes(projeto.CaminhoCompleto);
            projeto.Variaveis = ObterVariaveis(projeto.CaminhoCompleto);

            // TODO: obter conexoes, variaveis e demais dependencias

            return projeto;
        }
    }
}
