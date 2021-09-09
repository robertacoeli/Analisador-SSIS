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
        }
    }
}
