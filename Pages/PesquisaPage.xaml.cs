using AnalisadorSSIS.Modelo;
using AnalisadorSSIS.Servicos;
using AnalisadorSSIS.Servicos.Extensoes;
using System;
using System.Collections.Generic;
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
                                                ConteudoResultado = r.Resultados.ConteudoCorrespondente // TODO: destacar o que corresponde ao termo de pesquisa?
                                                        }).ToList();

                DataGridResultados.ItemsSource = resultados;
                GridTabelaResultados.Visibility = Visibility.Visible;
                ResumoResultados.Content = $"Sua pesquisa retornou {resultados.Count} resultados.";
            }

            GridResultados.Visibility = Visibility.Visible;
        }
    }
}
