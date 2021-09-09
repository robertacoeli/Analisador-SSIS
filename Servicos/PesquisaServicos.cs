using AnalisadorSSIS.Modelo;
using AnalisadorSSIS.Modelo.Enums;
using AnalisadorSSIS.Servicos.Extensoes;
using System.Collections.Generic;

namespace AnalisadorSSIS.Servicos
{
    internal class PesquisaServicos
    {
        public string Termo { get; set; }
        internal string TermoFormatado => Termo.Formatar();
        public Projeto Projeto { get; set; }
        public IList<Resultado> Resultados { get; set; } = new List<Resultado>();

        public PesquisaServicos(string termo, Projeto projeto)
        {
            Termo = termo;
            Projeto = projeto;
        }

        private bool PesquisarNoTexto(string textoPesquisa)
        {
            if (string.IsNullOrEmpty(textoPesquisa))
                return false;

            return textoPesquisa.Contains(Termo) || textoPesquisa.Formatar().Contains(TermoFormatado);
        }

        private void PesquisarGeralPacote(Pacote pacote)
        {
            if (PesquisarNoTexto(pacote.Nome))
                Resultados.Add(new Resultado()
                {
                    Pacote = pacote,
                    Nome = pacote.Nome,
                    TipoItem = TipoItem.Pacote,
                    TipoResultado = TipoResultado.Titulo,
                    Habilitado = pacote.Habilitado,
                    IdConexao = null,
                    ConteudoCorrespondente = pacote.Nome
                });
        }

        private void PesquisarConexao(Conexao conexao, Pacote pacote)
        {
            if (PesquisarNoTexto(conexao.Nome))
            {
                Resultados.Add(new Resultado()
                {
                    Pacote = pacote,
                    Nome = conexao.Nome,
                    TipoItem = TipoItem.Conexao,
                    TipoResultado = TipoResultado.Titulo,
                    Habilitado = true,
                    IdConexao = conexao.Id,
                    ConteudoCorrespondente = conexao.Nome
                });
            }
            else if (PesquisarNoTexto(conexao.ConnectionString))
            {
                Resultados.Add(new Resultado()
                {
                    Pacote = pacote,
                    Nome = conexao.Nome,
                    TipoItem = TipoItem.Conexao,
                    TipoResultado = TipoResultado.Conteudo,
                    Habilitado = true,
                    IdConexao = conexao.Id,
                    ConteudoCorrespondente = conexao.ConnectionString
                });
            }
        }

        private void PesquisarConexoes(Pacote pacote)
        {
            foreach (Conexao conexao in pacote.Conexoes)
                PesquisarConexao(conexao, pacote);
        }

        private void PesquisarVariavel(Variavel variavel, Pacote pacote)
        {
            if (PesquisarNoTexto(variavel.Nome))
            {
                Resultados.Add(new Resultado()
                {
                    Pacote = pacote,
                    Nome = variavel.NomeReferencia,
                    TipoItem = TipoItem.Variavel,
                    TipoResultado = TipoResultado.Titulo,
                    Habilitado = true,
                    IdConexao = null,
                    ConteudoCorrespondente = variavel.Nome
                });
            }
            else if (PesquisarNoTexto(variavel.ConteudoExpressao))
            {
                Resultados.Add(new Resultado()
                {
                    Pacote = pacote,
                    Nome = variavel.NomeReferencia,
                    TipoItem = TipoItem.Variavel,
                    TipoResultado = TipoResultado.Expressao,
                    Habilitado = pacote.Habilitado,
                    IdConexao = null,
                    ConteudoCorrespondente = variavel.ConteudoExpressao
                });
            }
            else if (PesquisarNoTexto(variavel.Conteudo))
            {
                Resultados.Add(new Resultado()
                {
                    Pacote = pacote,
                    Nome = variavel.NomeReferencia,
                    TipoItem = TipoItem.Variavel,
                    TipoResultado = TipoResultado.Conteudo,
                    Habilitado = pacote.Habilitado,
                    IdConexao = null,
                    ConteudoCorrespondente = variavel.Conteudo
                });
            }
        }

        private void PesquisarVariaveis(Pacote pacote)
        {
            foreach (Variavel variavel in pacote.Variaveis)
                PesquisarVariavel(variavel, pacote);
        }

        private void PesquisarExecutavel(Executavel executavel, Pacote pacote)
        {
            if (PesquisarNoTexto(executavel.ConteudoExpressao))
            {
                Resultados.Add(new Resultado()
                {
                    Pacote = pacote,
                    Nome = executavel.Nome,
                    TipoItem = TipoItem.Executavel,
                    TipoResultado = TipoResultado.Expressao,
                    Habilitado = executavel.Habilitado && pacote.Habilitado,
                    IdConexao = executavel.IdConexao,
                    ConteudoCorrespondente = executavel.ConteudoExpressao
                });
            }
            else if (PesquisarNoTexto(executavel.Conteudo))
            {
                Resultados.Add(new Resultado()
                {
                    Pacote = pacote,
                    Nome = executavel.Nome,
                    TipoItem = TipoItem.Executavel,
                    TipoResultado = TipoResultado.Conteudo,
                    Habilitado = executavel.Habilitado && pacote.Habilitado,
                    IdConexao = executavel.IdConexao,
                    ConteudoCorrespondente = executavel.Conteudo
                });
            }

            PesquisarExecutaveis(executavel.Descendentes, pacote);
        }

        private void PesquisarExecutaveis(IList<Executavel> executaveis, Pacote pacote)
        {
            foreach (Executavel executavel in executaveis)
                PesquisarExecutavel(executavel, pacote);
        }

        private void PesquisarPacotesProjeto()
        {
            foreach (Pacote pacote in Projeto.Pacotes)
            {
                PesquisarGeralPacote(pacote);
                PesquisarConexoes(pacote);
                PesquisarVariaveis(pacote);
                PesquisarExecutaveis(pacote.Executaveis, pacote);
            }
        }

        private void PesquisarParametroProjeto(Variavel variavel)
        {
            if (PesquisarNoTexto(variavel.Nome))
            {
                Resultados.Add(new Resultado()
                {
                    Pacote = null,
                    Nome = variavel.NomeReferencia,
                    TipoItem = TipoItem.ParametroProjeto,
                    TipoResultado = TipoResultado.Titulo,
                    Habilitado = true,
                    IdConexao = null,
                    ConteudoCorrespondente = variavel.Nome
                });
            }
            else if (PesquisarNoTexto(variavel.Conteudo))
            {
                Resultados.Add(new Resultado()
                {
                    Pacote = null,
                    Nome = variavel.NomeReferencia,
                    TipoItem = TipoItem.Variavel,
                    TipoResultado = TipoResultado.Conteudo,
                    Habilitado = true,
                    IdConexao = null,
                    ConteudoCorrespondente = variavel.Conteudo
                });
            }
        }

        private void PesquisarParametrosProjeto()
        {
            foreach (Variavel variavel in Projeto.Variaveis)
                PesquisarParametroProjeto(variavel);
        }

        private void PesquisarConexoesProjeto()
        {
            foreach (Conexao conexao in Projeto.Conexoes)
                PesquisarConexao(conexao, null);
        }

        public void Pesquisar()
        {
            PesquisarConexoesProjeto();
            PesquisarParametrosProjeto();
            PesquisarPacotesProjeto();
        }
    }
}
