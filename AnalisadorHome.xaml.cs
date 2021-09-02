using AnalisadorSSIS.Modelo;
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
                InitialDirectory = "Content", // TODO: ver a ultima solucao aberta e colocar no diretorio da mesma (colocar num arquivo log)
                Title = "Selecionar arquivo de solução do projeto",
                Multiselect = false
            };

            if (openFile.ShowDialog().HasValue)
            {
                solucao = new Solucao(openFile.FileName);

                foreach (string subdiretorio in Directory.EnumerateDirectories(solucao.diretorio).OrderBy(x => x))
                {
                    if (Directory.GetFiles(subdiretorio, string.Format("*{0}", Config.extensaoArquivoPacote)).Length > 0)
                        solucao.projetos.Add(subdiretorio);
                }

                if (solucao.projetos.Count <= 0)
                {
                    MessageBox.Show("Solução Inválida. A solução apresentada não possui projetos SSIS (extensão .dtsx).");
                    return;
                }
            }
        }
    }
}
