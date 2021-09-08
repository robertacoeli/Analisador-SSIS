using AnalisadorSSIS.Modelo;
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
    }
}
