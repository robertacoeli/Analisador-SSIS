using AnalisadorSSIS.Modelo;
using AnalisadorSSIS.Modelo.Enums;
using AnalisadorSSIS.Servicos;
using AnalisadorSSIS.Servicos.Extensoes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnalisadorSSIS.Pages
{
    /// <summary>
    /// Interaction logic for PesquisaPage.xaml
    /// </summary>
    public partial class PesquisaPage : Page
    {
        static int CARACTERES_MARGEM_TERMO = 50;
        Projeto projeto;

        public PesquisaPage()
        {
            InitializeComponent();
        }

        public PesquisaPage(object data) : this()
        {
            DataContext = data;
            projeto = (Projeto)data;
            TituloPagina.Content = TituloPagina.Content + $" - {projeto.Solucao.NomeArquivo} - {projeto.Nome}";
        }

        private string EncontrarTermoNoConteudo(string termo, string conteudo, TipoResultado tipoResultado)
        {
            if (string.IsNullOrEmpty(conteudo) || tipoResultado == TipoResultado.Titulo)
                return conteudo;

            StringBuilder stringBuilder = new StringBuilder();
            StringReader stringReader = new StringReader(conteudo);

            string linha;
            int numLinha = 1;
            int tamanhoTermo = termo.Length;
            while ((linha = stringReader.ReadLine()) != null) {
                int indiceTermo = linha.FormatarIgnorandoEspacos().IndexOf(termo.FormatarIgnorandoEspacos());
                if (indiceTermo < 0)
                    continue;

                int tamanhoLinha = linha.Length;
                int limiteInferior = indiceTermo > CARACTERES_MARGEM_TERMO ? (indiceTermo - CARACTERES_MARGEM_TERMO) : 0;
                int limiteSuperior = indiceTermo + tamanhoTermo + CARACTERES_MARGEM_TERMO;
                limiteSuperior = limiteSuperior > tamanhoLinha ? tamanhoLinha : limiteSuperior;
                string stringInicial = limiteInferior == 0 && numLinha == 1 ? string.Empty : "(...)";
                stringBuilder.AppendLine($"Linha {numLinha}: {stringInicial}{linha.Substring(limiteInferior, limiteSuperior - limiteInferior)}");
                numLinha += 1;
            }

            return stringBuilder.ToString();
        }

        private void VerificarEntradaPesquisa(object sender, TextChangedEventArgs e)
        {
            var termoPesquisa = TermoPesquisa.Text;

            if (string.IsNullOrWhiteSpace(termoPesquisa))
                BotaoPesquisar.IsEnabled = false;
            else
                BotaoPesquisar.IsEnabled = true;
        }

        private void Pesquisar(object sender, RoutedEventArgs e)
        {
            string termoPesquisa = TermoPesquisa.Text;
            PesquisaServicos servicoPesquisa = new PesquisaServicos(termoPesquisa, projeto);
            servicoPesquisa.Pesquisar();

            if (servicoPesquisa.Resultados == null || servicoPesquisa.Resultados.Count <= 0)
            {
                DataGridResultados.ItemsSource = null;
                GridTabelaResultados.Visibility = Visibility.Hidden;
                ResumoResultados.Content = "Nenhum resultado foi retornado para sua pesquisa.";
            }
            else
            {
                var resultados = servicoPesquisa.Resultados
                                .GroupJoin(projeto.Conexoes,
                                            resultado => resultado.IdConexao,
                                            conexao => conexao.Id,
                                            (resultado, conexao) => new { Resultados = resultado, Conexoes = conexao })
                                .SelectMany(c => c.Conexoes.DefaultIfEmpty(),
                                            (r, c) => new
                                            {
                                                TipoResultadoStr = r.Resultados.TipoResultado.GetDescription(),
                                                NomePacote = r.Resultados.Pacote != null ? r.Resultados.Pacote.Nome : "N/A",
                                                Nome = r.Resultados.Nome,
                                                TipoItemStr = r.Resultados.TipoItem.GetDescription(),
                                                Habilitado = r.Resultados.Habilitado,
                                                NomeConexao = c != null ? c.Nome : "N/A",
                                                ConteudoResultado = EncontrarTermoNoConteudo(termoPesquisa, r.Resultados.ConteudoCorrespondente, r.Resultados.TipoResultado)
                                                        }).ToList();

                DataGridResultados.ItemsSource = resultados;
                GridTabelaResultados.Visibility = Visibility.Visible;
                ResumoResultados.Content = $"Sua pesquisa retornou {resultados.Count} resultados.";
            }

            GridResultados.Visibility = Visibility.Visible;
        }
    }
}
