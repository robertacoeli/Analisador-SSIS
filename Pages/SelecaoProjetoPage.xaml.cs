using AnalisadorSSIS.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SelecaoProjetoPage.xaml
    /// </summary>
    public partial class SelecaoProjetoPage : Page
    {
        Solucao solucao;

        public SelecaoProjetoPage()
        {
            InitializeComponent();
        }

        public SelecaoProjetoPage(object data) : this()
        {
            DataContext = data;
            solucao = (Solucao)data;
            ListaProjetos.ItemsSource = solucao.Projetos.Select(x => System.IO.Path.GetFileName(x));

            if (solucao.Projetos.Count == 1)
                ListaProjetos.SelectedIndex = 0;
            else
                BotaoPesquisar.IsEnabled = false;

            NomeSolucao.Content = $"Solução: {solucao.NomeArquivo}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var indice = ListaProjetos.SelectedIndex;
            // TODO: terminar implementacao, chamando proxima pagina
        }

        private void SelecionarItem(object sender, SelectionChangedEventArgs e)
        {
            if (ListaProjetos.SelectedIndex < 0)
                BotaoPesquisar.IsEnabled = false;
            else
                BotaoPesquisar.IsEnabled = true;
        }
    }
}
