using AnalisadorSSIS.Modelo;
using AnalisadorSSIS.Pages;
using Microsoft.Win32;
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

namespace AnalisadorSSIS
{
    /// <summary>
    /// Interaction logic for AnalisadorHome.xaml
    /// </summary>
    public partial class AnalisadorHome : Page
    {
        Solucao solucao;

        public AnalisadorHome()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFile = new OpenFileDialog
            {
                Filter = "Solution Files (*.sln)|*.sln",
                InitialDirectory = "Content",
                Title = "Selecionar arquivo de solução do projeto",
                Multiselect = false
            };

            if (openFile.ShowDialog().HasValue)
            {
                string nomeArquivo = openFile.FileName;

                if (string.IsNullOrEmpty(nomeArquivo))
                    return;

                solucao = new Solucao(nomeArquivo);
                foreach (string subdiretorio in Directory.EnumerateDirectories(solucao.Diretorio).OrderBy(x => x))
                {
                    if (Directory.GetFiles(subdiretorio, string.Format("*{0}", Config.extensaoArquivoPacote)).Length > 0)
                        solucao.Projetos.Add(subdiretorio);
                }

                if (solucao.Projetos.Count <= 0)
                {
                    MessageBox.Show("Solução Inválida. A solução apresentada não possui projetos SSIS (extensão .dtsx).");
                    return;
                }

                SelecaoProjetoPage paginaSelecaoProjeto = new SelecaoProjetoPage(solucao);
                NavigationService.Navigate(paginaSelecaoProjeto);
            }
        }
    }
}
